using GraphProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace BehaviorEditor
{
    [NodeCustomEditor(typeof(ClassNode))]
    internal class ClassNodeView: BaseNodeView
    {
        public override void Enable()
        {
            var node = nodeTarget as ClassNode;
            foreach (var field in node.FieldInfos)
            {
                if (!field.IsPublic)
                {
                    continue;
                }
                if (field.TypeName == "string")
                {
                    var titleLabel = new Label(field.Desc);
                    controlsContainer.Add(titleLabel);
                    TextField fieldText = new TextField(field.Name);
                    controlsContainer.Add(fieldText);
                }
                else if (field.TypeName == "number")
                {
                    var titleLabel = new Label(field.Desc);
                    controlsContainer.Add(titleLabel);
                    TextField fieldText = new TextField(field.Name);
                    controlsContainer.Add(fieldText);
                }
                else if (field.TypeName == "bool")
                {
                    
                }
            }
        }
    }
}
