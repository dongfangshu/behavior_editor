using Assets.Examples.Editor.BehaviorEditor.TypeSystem;
using GraphProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Assets.Examples.Editor.BehaviorEditor
{
    [NodeCustomEditor(typeof(TenplateNode))]
    internal class TemplateNodeView: BaseNodeView
    {
        public override void Enable()
        {
            var myNode = nodeTarget as TenplateNode;
            foreach (var m in myNode.Member)
            {
                if (m is NumberType number)
                {
                    LongField longField = new LongField();
                    longField.name = m.Name;
                    controlsContainer.Add(longField);
                }
                else if (m is BoolType boolType)
                {
                    //BaseBoolField boolField = new BaseBoolField();
                    Toggle toggle = new Toggle();
                    toggle.name = m.Name;
                    controlsContainer.Add(toggle);
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
    }
}
