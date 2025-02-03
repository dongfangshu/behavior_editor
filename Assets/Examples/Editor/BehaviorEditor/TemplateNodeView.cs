using GraphProcessor;
using LuaAnalyzer.TypeSystem;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Assets.Examples.Editor.BehaviorEditor
{
    [NodeCustomEditor(typeof(TenplateNode))]
    internal class TemplateNodeView: BaseNodeView
    {
        public override void Enable()
        {
            var myNode = nodeTarget as TenplateNode;
            foreach (var m in myNode.AST.MemberVariable)
            {
                if (!m.IsPublic)
                {
                    continue;
                }
                if (m is NumberType number)
                {
                    TextField longField = new TextField(number.Name);
                    //longField.name = number.Name;
                    longField.value = number.Value;
                    controlsContainer.Add(longField);
                    longField.RegisterCallback<InputEvent>(OnInputChange);
                }
                else if (m is BoolType boolType)
                {
                    //BaseBoolField boolField = new BaseBoolField();
                    Toggle toggle = new Toggle(boolType.Name);
                    toggle.value = boolType.AsBool;
                    controlsContainer.Add(toggle);
                }
                else if (m is StringType stringType)
                {
                    TextField textField = new TextField(stringType.Name);
                    //textField.name = m.Name;
                    controlsContainer.Add(textField);
                }
                else if (m is TableType tableType)
                {
                    if (tableType.IsArray)
                    {
                        var listView = new ListView();
                        Add(listView);
                        foreach (var field in tableType.FieldInfos)
                        {
                            
                        }
                    }
                }
            }

            //var floatNode = nodeTarget as FloatNode;

            //DoubleField floatField = new DoubleField
            //{
            //    value = floatNode.input
            //};

            //floatNode.onProcessed += () => floatField.value = floatNode.input;

            //floatField.RegisterValueChangedCallback((v) => {
            //    owner.RegisterCompleteObjectUndo("Updated floatNode input");
            //    floatNode.input = (float)v.newValue;
            //});

            //controlsContainer.Add(floatField);
        }

        private void OnInputChange(InputEvent evt)
        {
            TextField textField = evt.target as TextField;
            if (textField != null)
            {
                // 检查输入是否为数字
                bool isLong = long.TryParse(evt.newData, out _);
                if (isLong)
                {
                    return;
                }
                bool isFloat = float.TryParse(evt.newData, out _);
                if (isFloat)
                {
                    return;
                }
                // 如果不是数字，则阻止输入
                evt.PreventDefault();
            }
        }
    }
}
