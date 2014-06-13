// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceSorter.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the ServiceSorter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the ServiceSorter type.
    /// </summary>
    internal class ServiceSorter
    {
        /// <summary>
        /// Sort a collection of service types so that each service may be started after all
        /// their dependencies are started.
        /// </summary>
        /// <param name="serviceTypes">
        /// The service types.
        /// </param>
        /// <returns>
        /// The sorted service types.
        /// </returns>
        public IEnumerable<Type> Sort(IEnumerable<Type> serviceTypes)
        {
            var dependencies = serviceTypes.Select(x => new Dependency(x, FindDependencies(x)))
                .ToArray();

            var sorter = new TopologicalSorter(dependencies.Length);
            var indexes = new Dictionary<Type, int>();

            for (var i = 0; i < dependencies.Length; i++)
            {
                indexes[dependencies[i].Dependant] = sorter.AddVertex(i);
            }

            for (var i = 0; i < dependencies.Length; i++)
            {
                if (dependencies[i].Dependencies == null || !dependencies[i].Dependencies.Any())
                {
                    continue;
                }

                foreach (var t in dependencies[i].Dependencies)
                {
                    sorter.AddEdge(i, indexes[t]);
                }
            }

            var sortedServices = sorter.Sort()
                .Reverse()
                .Select(x => dependencies[x].Dependant)
                .ToList();

            return sortedServices;
        }

        /// <summary>
        /// Find all the dependencies of a given service type.
        /// </summary>
        /// <param name="serviceType">
        /// The service type.
        /// </param>
        /// <returns>
        /// The found dependencies.
        /// </returns>
        protected virtual Type[] FindDependencies(Type serviceType)
        {
            var dependencies = serviceType.GetInterfaces()
                .Select(x => new Tuple<Type, Type>(x, x.GetGenericArguments().FirstOrDefault()))
                .Where(x => x.Item2 != null && typeof(IDependOn<>).MakeGenericType(x.Item2) == x.Item1)
                .Select(x => x.Item2)
                .ToArray();

            return dependencies;
        }

        /// <summary>
        /// Defines the CyclicDependenciesException type.
        /// </summary>
        public class CyclicDependenciesException : Exception
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ServiceSorter.CyclicDependenciesException"/> class.
            /// </summary>
            public CyclicDependenciesException()
                : base("Cyclic dependencies detected")
            {
            }
        }

        /// <summary>
        /// Defines the TopologicalSorter type.
        /// </summary>
        private class TopologicalSorter
        {
            /// <summary>
            /// The vertices.
            /// </summary>
            private readonly int[] _vertices;

            /// <summary>
            /// The matrix.
            /// </summary>
            private readonly int[,] _matrix;

            /// <summary>
            /// The sorted indexes.
            /// </summary>
            private readonly int[] _sortedIndexes;

            /// <summary>
            /// The number of vertices.
            /// </summary>
            private int _numberOfVertices;

            /// <summary>
            /// Initializes a new instance of the <see cref="TopologicalSorter"/> class.
            /// </summary>
            /// <param name="size">
            /// The size.
            /// </param>
            public TopologicalSorter(int size)
            {
                _vertices = new int[size];
                _matrix = new int[size, size];
                _numberOfVertices = 0;
                _sortedIndexes = new int[size];
            }

            /// <summary>
            /// Add a vertex.
            /// </summary>
            /// <param name="vertex">
            /// The vertex.
            /// </param>
            /// <returns>
            /// The vertex index.
            /// </returns>
            public int AddVertex(int vertex)
            {
                _vertices[_numberOfVertices++] = vertex;

                return _numberOfVertices - 1;
            }

            /// <summary>
            /// Add an edge.
            /// </summary>
            /// <param name="start">
            /// The start.
            /// </param>
            /// <param name="end">
            /// The end.
            /// </param>
            public void AddEdge(int start, int end)
            {
                _matrix[start, end] = 1;
            }

            /// <summary>
            /// Sort the matrix.
            /// </summary>
            /// <returns>
            /// The sorted indexes.
            /// </returns>
            public IEnumerable<int> Sort()
            {
                while (_numberOfVertices > 0)
                {
                    var currentVertex = NoSuccessors();

                    if (currentVertex == -1)
                    {
                        throw new CyclicDependenciesException();
                    }

                    _sortedIndexes[_numberOfVertices - 1] = _vertices[currentVertex];

                    DeleteVertex(currentVertex);
                }

                return _sortedIndexes;
            }

            /// <summary>
            /// Find a vertex without a successor.
            /// </summary>
            /// <returns>
            /// The successor index.
            /// </returns>
            private int NoSuccessors()
            {
                for (var row = 0; row < _numberOfVertices; row++)
                {
                    var isEdge = false;

                    for (var column = 0; column < _numberOfVertices; column++)
                    {
                        if (_matrix[row, column] <= 0)
                        {
                            continue;
                        }

                        isEdge = true;

                        break;
                    }

                    if (!isEdge)
                    {
                        return row;
                    }
                }

                return -1;
            }

            /// <summary>
            /// Delete a vertex.
            /// </summary>
            /// <param name="vertex">
            /// The vertex to delete.
            /// </param>
            private void DeleteVertex(int vertex)
            {
                if (vertex != _numberOfVertices - 1)
                {
                    for (var j = vertex; j < _numberOfVertices - 1; j++)
                    {
                        _vertices[j] = _vertices[j + 1];
                    }

                    for (var row = vertex; row < _numberOfVertices - 1; row++)
                    {
                        MoveRowUp(row, _numberOfVertices);
                    }

                    for (var column = vertex; column < _numberOfVertices - 1; column++)
                    {
                        MoveColumnLeft(column, _numberOfVertices - 1);
                    }
                }

                _numberOfVertices--;
            }

            /// <summary>
            /// Move a row one step up.
            /// </summary>
            /// <param name="row">
            /// The row.
            /// </param>
            /// <param name="length">
            /// The length.
            /// </param>
            private void MoveRowUp(int row, int length)
            {
                for (var column = 0; column < length; column++)
                {
                    _matrix[row, column] = _matrix[row + 1, column];
                }
            }

            /// <summary>
            /// Move a column one step left.
            /// </summary>
            /// <param name="column">
            /// The column.
            /// </param>
            /// <param name="length">
            /// The length.
            /// </param>
            private void MoveColumnLeft(int column, int length)
            {
                for (var row = 0; row < length; row++)
                {
                    _matrix[row, column] = _matrix[row, column + 1];
                }
            }
        }

        /// <summary>
        /// Defines the Dependency type.
        /// </summary>
        private class Dependency
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Dependency"/> class.
            /// </summary>
            /// <param name="dependant">
            /// The dependant.
            /// </param>
            /// <param name="dependencies">
            /// The dependencies.
            /// </param>
            public Dependency(Type dependant, Type[] dependencies)
            {
                Dependant = dependant;
                Dependencies = dependencies;
            }

            /// <summary>
            /// Gets the dependant.
            /// </summary>
            public Type Dependant { get; private set; }

            /// <summary>
            /// Gets the dependencies.
            /// </summary>
            public Type[] Dependencies { get; private set; }
        }
    }
}