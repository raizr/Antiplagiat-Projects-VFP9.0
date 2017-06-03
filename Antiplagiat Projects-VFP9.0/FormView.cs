using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace Antiplagiat_Projects_VFP9._0 {
    public partial class FormView : Form {
        private CManager manager;
        private List<IDictionary<string, string>> ObjProperties;
        private List<IDictionary<string, string>> RefProperties;
        private List<SObject> list;
        private List<SObject> InsObj;
        private List<List<SMethod>> InsMethods;
        private List<List<SMethod>> RefMethods;
        private List<SCheckElementInfo> RefInfoList;
        private int FormIndex;
        public FormView(CManager ptr, int FormIndex) {
            InitializeComponent();
            manager = ptr;
            this.FormIndex = FormIndex;
            ObjProperties = new List<IDictionary<string, string>>();
            InsMethods = new List<List<SMethod>>();
            RefMethods = new List<List<SMethod>>();
            InsObj = new List<SObject>();
            ObjProperties = SelectFormView(manager.InspectProject.Forms[FormIndex], listViewObjects);
            InsObj.AddRange(list);
            RefInfoList = manager.GetListFilesInfo().FindAll(x => x.Type == 'O');
        }

        private void listViewObjects_DoubleClick(object sender, EventArgs e) {
            ListView listview = (ListView)sender;
            if (listViewObjects.SelectedIndices.Count > 0) {
               IDictionary<string, string> prop = ObjProperties[listViewObjects.SelectedIndices[0]];
                listViewProperties.Items.Clear();
                foreach (KeyValuePair<string, string> kvp in prop) {
                    ListViewItem property = new ListViewItem(kvp.Key);
                    property.SubItems.Add(kvp.Value);
                    property.Group = listViewProperties.Groups[0];
                    listViewProperties.Items.Add(property);
                }
                List<SMethod> ObjMethods = InsMethods[listViewObjects.SelectedIndices[0]];
                foreach (SMethod method in ObjMethods) {
                    string[] str = method.Method.Trim('\r', '\n').Split(' ');
                    ListViewItem ObjMethod = new ListViewItem(str[0]);
                    ObjMethod.SubItems.Add(str[1]);
                    ObjMethod.Group = listViewProperties.Groups[1];
                    if(method.RefMethod != null)
                        ObjMethod.BackColor = 
                            Properties.Settings.Default.TokenColor;
                    listViewProperties.Items.Add(ObjMethod);
                }
                listViewRefObjects.Items.Clear();
                listViewRefProperties.Items.Clear();
                richTextBoxRefInfo.Text = "";
                if (InsObj[listViewObjects.SelectedIndices[0]].RefFormFullName != null &&
                    InsObj[listViewObjects.SelectedIndices[0]].RefObjectIndex >= 0) {
                    //RefFObjectIndex[;
                    
                    RefProperties = SelectFormView(manager.ReferenceProject.
                                    OpenForm(InsObj[listViewObjects.SelectedIndices[0]].RefFormFullName),
                                   listViewRefObjects);
                    if(InsObj[listViewObjects.SelectedIndices[0]].RefObjectIndex >= 0)
                        listViewRefObjects.
                            Items[InsObj[listViewObjects.SelectedIndices[0]].RefObjectIndex].BackColor =
                            Properties.Settings.Default.RefFormObjColor;
                    SCheckElementInfo find = RefInfoList.Find(x => x.EqualNum == FormIndex && x.Type == 'O');
                    richTextBoxRefInfo.Text = "Имя проекта: " + Path.GetFileNameWithoutExtension(find.ProjectName) + "\n" +
                        "Студента: " + find.StudentName + "\n" +
                        "Название формы:" + 
                        Path.GetFileNameWithoutExtension(InsObj[listViewObjects.SelectedIndices[0]].RefFormFullName);
                }
            }
        }

        private List<IDictionary<string, string>> SelectFormView(SForm FormStruct, ListView ListViewFormObjects) {
            List<IDictionary<string, string>> Prop = new List<IDictionary<string, string>>();
            List<List<SMethod>> methods = new List<List<SMethod>>();
            list = FormStruct.Objects;
            for (int i = 0; i < list.Count; i++) {
                ListViewItem obj = new ListViewItem(list[i].classname);
                switch (list[i].classname) {
                    case "form":
                        obj.Group = ListViewFormObjects.Groups[0];
                        break;
                    case "commandbutton":
                        obj.Group = ListViewFormObjects.Groups[1];
                        break;
                    case "header":
                        obj.Group = ListViewFormObjects.Groups[2];
                        break;
                    case "textbox":
                        obj.Group = ListViewFormObjects.Groups[3];
                        break;
                    case "grid":
                        obj.Group = ListViewFormObjects.Groups[4];
                        break;
                    case "label":
                        obj.Group = ListViewFormObjects.Groups[5];
                        break;
                    case "pageframe":
                        obj.Group = ListViewFormObjects.Groups[6];
                        break;
                    case "editbox":
                        obj.Group = ListViewFormObjects.Groups[7];
                        break;
                    case "spinner":
                        obj.Group = ListViewFormObjects.Groups[8];
                        break;
                    case "optiongroup":
                        obj.Group = ListViewFormObjects.Groups[9];
                        break;
                    case "checkbox":
                        obj.Group = ListViewFormObjects.Groups[10];
                        break;
                    case "combobox":
                        obj.Group = ListViewFormObjects.Groups[11];
                        break;
                    case "listbox":
                        obj.Group = ListViewFormObjects.Groups[12];
                        break;
                }
                obj.SubItems.Add(list[i].objname);
                Prop.Add(list[i].properties);
                methods.Add(list[i].methods);
                /*if (list[i].RefFormFullName != null) {
                    RefFObjectIndex.Add(list[i].RefFormFullName, list[i].RefObjectIndex);
                } else {
                    RefFObjectIndex.Add("", -1);
                }*/
                if (list[i].IsPlagiarism) {
                    obj.BackColor = Properties.Settings.Default.FormObjColor;
                }
                ListViewFormObjects.Items.Add(obj);
            }
            if (ListViewFormObjects.Name == "listViewObjects")
                InsMethods = methods;
            else
                RefMethods = methods;
            return Prop;
        }

        private void listViewRefObjects_DoubleClick(object sender, EventArgs e) {
            ListView listview = (ListView)sender;
            if (listViewRefObjects.SelectedIndices.Count > 0) {
                IDictionary<string, string> prop = RefProperties[listViewRefObjects.SelectedIndices[0]];
                listViewRefProperties.Items.Clear();
                foreach (KeyValuePair<string, string> kvp in prop) {
                    ListViewItem property = new ListViewItem(kvp.Key);
                    property.Group = listViewRefProperties.Groups[0];
                    property.SubItems.Add(kvp.Value);
                    listViewRefProperties.Items.Add(property);
                }
                
                List<SMethod> ObjMethods = RefMethods[listViewRefObjects.SelectedIndices[0]];
                foreach (SMethod method in ObjMethods) {
                    string[] str = method.Method.Trim('\r', '\n').Split(' ');
                    ListViewItem ObjMethod = new ListViewItem(str[0]);
                    ObjMethod.SubItems.Add(str[1]);
                    ObjMethod.Group = listViewRefProperties.Groups[1];
                    listViewRefProperties.Items.Add(ObjMethod);
                }
            }
        }

        private void listViewProperties_DoubleClick(object sender, EventArgs e) {
            ListView listview = (ListView)sender;
            if (listViewProperties.SelectedIndices.Count > 0) {
                if (listview.Items[listViewProperties.SelectedIndices[0]].Group == 
                    listview.Groups[1]) {
                    CodeView FormCode = new CodeView();
                    List < SMethod > ObjMethods = InsMethods[listViewObjects.SelectedIndices[0]];
                    //string ObjMethod = ObjMethods[0];
                    SMethod ObjMethod = ObjMethods[listViewProperties.SelectedIndices[0] -
                        listViewProperties.Groups[0].Items.Count];
                    string hash = "";
                    for(int i = 0; i < ObjMethod.Hash.Count; i++) {
                        hash += ObjMethod.Hash[i].ToString() + "\n";
                    }
                    FormCode.richInsText.Text = ObjMethod.Method+"\n\n"+ hash;
                    SCheckElementInfo find = RefInfoList.Find(x => x.EqualNum == FormIndex && x.Type == 'M');
                    FormCode.richTextBoxInfo.Text = "Имя проекта: " + Path.GetFileNameWithoutExtension(find.ProjectName) + "\n" +
                        "Студента: " + find.StudentName + "\n" +
                        "Название объекта:" +find.FileName;
                    if (ObjMethod.RefMethod != null) {
                        FormCode.richRefText.Text = ObjMethod.RefMethod;
                    }
                    FormCode.Show();
                }
            }
        }
    }
}
