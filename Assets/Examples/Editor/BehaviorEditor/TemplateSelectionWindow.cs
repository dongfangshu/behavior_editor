using LuaAnalyzer;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

namespace Assets.Examples.Editor.BehaviorEditor
{
    internal class TemplateSelectionWindow:OdinEditorWindow
    {
        public static void ShowWindow(Action<LuaCode> confirmCallBack)
        {
            var window = GetWindow<TemplateSelectionWindow>();
            window.confirmCallBack = confirmCallBack;
            window.InitializeWindow();
        }
        Action<LuaCode> confirmCallBack;
        [HideInInspector]
        public List<string> allBehavior = new List<string>();
        [HideInInspector]
        [ListDrawerSettings(NumberOfItemsPerPage = 5, Expanded = true)]
        public List<string> allBehaviorName = new List<string>();

        // 当前选择的选项
        [ShowInInspector]
        [ValueDropdown("GetDropdownOptions")]
        private string selectedOption;

        // 获取下拉菜单的选项
        private IEnumerable<string> GetDropdownOptions()
        {
            return allBehaviorName;
        }
        public void InitializeWindow()
        {
            string path = "Examples\\Editor\\BehaviorEditor\\level_behavior\\template";
            string dirPath = Path.Combine(Application.dataPath, path);
            string[] allFiles = Directory.GetFiles(dirPath);
            foreach (var file in allFiles)
            {
                if (file.EndsWith(".meta"))
                {
                    continue;
                }
                allBehavior.Add(file);
                FileInfo fileInfo = new FileInfo(file);
                allBehaviorName.Add(fileInfo.Name);
            }
        }
        [Button]
        public void Confirm()
        {
            var index = allBehaviorName.IndexOf(selectedOption);
            if (index != -1)
            {
                LuaCode luaCode = new LuaCode();
                var context = File.ReadAllText(allBehavior[index]);
                if (luaCode.TryParse(context))
                {
                    confirmCallBack?.Invoke(luaCode);
                }
            }
            Close();
        }
    }
}
