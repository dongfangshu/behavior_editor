using GraphProcessor;
using LuaAnalyzer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Examples.Editor.BehaviorEditor
{
    internal class BehaviorView : BaseGraphView
    {
        public BehaviorView(EditorWindow window) : base(window)
        {
        }
        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            BuildTemplateLevelContextualMenu(evt);
            base.BuildContextualMenu(evt);
        }
        private Vector2 tmpPos;
        protected void BuildTemplateLevelContextualMenu(ContextualMenuPopulateEvent evt)
        {
            Vector2 position = (evt.currentTarget as VisualElement).ChangeCoordinatesTo(contentViewContainer, evt.localMousePosition);
            tmpPos = position;
            evt.menu.AppendAction("添加模板节点", (e) => TemplateSelectionWindow.ShowWindow(AddTemplateBehaviorNode));
        }
        private void AddTemplateBehaviorNode(LuaCode luaCode)
        {
            var node = BaseNode.CreateFromType<TenplateNode>(tmpPos);
            node.Init(luaCode);
            var nodeView = AddNode(node);
            nodeView.style.width = 200;
        }
    }
}
