using GraphProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Examples.Editor.BehaviorEditor
{
    internal class BehaviorEditor : BaseGraphWindow
    {
        BaseGraph tmpGraph;
        [MenuItem("Window/06 Template Level")]
        public static BaseGraphWindow OpenWithTmpGraph()
        {
            var graphWindow = CreateWindow<BehaviorEditor>();

            // When the graph is opened from the window, we don't save the graph to disk
            graphWindow.tmpGraph = ScriptableObject.CreateInstance<BaseGraph>();
            graphWindow.tmpGraph.hideFlags = HideFlags.HideAndDontSave;
            graphWindow.InitializeGraph(graphWindow.tmpGraph);

            graphWindow.Show();

            return graphWindow;
        }

        protected override void InitializeWindow(BaseGraph graph)
        {
            titleContent = new GUIContent("All Graph");

            if (graphView == null)
            {
                graphView = new BehaviorView(this);
                //toolbarView = new CustomToolbarView(graphView);
                //graphView.Add(toolbarView);
            }

            rootView.Add(graphView);
        }
    }
}
