﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using GraphSharp.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;
using MicroMvvm;
using MicrosoftJava.Shared;

namespace WordCloud
{
    class GraphViewModel : ObservableObject
    {
        #region Members

        private string layoutAlgorithmType;
        private PocGraph graph;
        private List<PocVertex> existingVertices = new List<PocVertex>();
        private List<String> layoutAlgorithmTypes = new List<string>();
        private ProjectViewModel parent;
        private List<Word> wordList = new List<Word>();
        #endregion

        #region Constructor

        public GraphViewModel(ProjectViewModel parent)
        {
            this.parent = parent;
        }

        #endregion

        #region Private Methods

        public void RunGraph(string startWord, List<Word> wordList)
        {
            Graph = new PocGraph(true);
            existingVertices.Clear();

            this.wordList = wordList;
            var qualifiedWords = EditDistance.GetShortestLevenshtein(startWord, wordList.Select(w => w.Name).ToList());

            existingVertices.Add(new PocVertex(0, startWord, 0));
            int position = 0;
            foreach (KeyValuePair<string, int> w in qualifiedWords)
            {
                existingVertices.Add(new PocVertex(position++, w.Key, w.Value));
            }

            foreach (PocVertex vertex in existingVertices)
                graph.AddVertex(vertex);

            //add some edges to the graph

            for (int i = 0; i < existingVertices.Count; i++)
            {
                AddNewGraphEdge(existingVertices[0], existingVertices[i]);
            }

            //Add Layout Algorithm Types
            layoutAlgorithmTypes.Add("BoundedFR");
            layoutAlgorithmTypes.Add("Circular");
            layoutAlgorithmTypes.Add("CompoundFDP");
            layoutAlgorithmTypes.Add("EfficientSugiyama");
            layoutAlgorithmTypes.Add("FR");
            layoutAlgorithmTypes.Add("ISOM");
            layoutAlgorithmTypes.Add("KK");
            layoutAlgorithmTypes.Add("LinLog");
            layoutAlgorithmTypes.Add("Tree");

            //Pick a default Layout Algorithm Type
            LayoutAlgorithmType = "Tree";
        }

        private PocEdge AddNewGraphEdge(PocVertex from, PocVertex to)
        {
            string edgeString = string.Format("{0}-{1} Connected", from.word, to.word);

            PocEdge newEdge = new PocEdge(edgeString, from, to);
            Graph.AddEdge(newEdge);
            return newEdge;
        }
        #endregion

        #region Public Properties

        public string Title
        {
            get
            {
                return "Graph";
            }
        }

        public List<String> LayoutAlgorithmTypes
        {
            get { return layoutAlgorithmTypes; }
        }


        public string LayoutAlgorithmType
        {
            get { return layoutAlgorithmType; }
            set
            {
                layoutAlgorithmType = value;
                RaisePropertyChanged("LayoutAlgorithmType");
            }
        }

        public PocGraph Graph
        {
            get { 
                return graph; 
            }
            set { 
                if (this.graph == value) return;
                this.graph = value; 

                RaisePropertyChanged("Graph");
            }
        }
        #endregion

        #region Commands

        public ICommand GraphTextBlockClick { get { return new RelayCommand<object>((param) => this.GraphTextBlockClickExecute(param)); } }

        void GraphTextBlockClickExecute(object parameter)
        {
            int prevItemsCount = existingVertices.Count;
            PocVertex newRoot = (parameter as PocVertex);

            if ( Graph.Edges.Any(e => e.Source == newRoot) ) return;

            var qualifiedWords = EditDistance.GetShortestLevenshtein(newRoot.word, wordList.Select(w => w.Name).ToList());

            foreach (KeyValuePair<string, int> w in qualifiedWords)
            {
                var tmpVertex = new PocVertex(prevItemsCount++, w.Key, w.Value);
                existingVertices.Add(tmpVertex);
                graph.AddVertex(tmpVertex);
                AddNewGraphEdge(newRoot, tmpVertex);
            }
        }

        #endregion

    }
}
