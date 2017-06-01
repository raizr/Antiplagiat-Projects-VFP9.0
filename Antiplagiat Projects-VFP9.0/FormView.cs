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
        private List<IDictionary<string, string>> Properties;
        private List<IDictionary<string, string>> RefProperties;
        private List<SObject> list;
        private List<string> RefFormList;
        private List<List<SMethod>> methods;
        private List<int> RefFormIndex;
        private List<SCheckElementInfo> RefInfoList;
        private int FormIndex;
        public FormView(CManager ptr, int FormIndex) {
            InitializeComponent();
            manager = ptr;
            this.FormIndex = FormIndex;
            Properties = new List<IDictionary<string, string>>();
            methods = new List<List<SMethod>>();
            RefFormList = new List<string>();
            RefFormIndex = new List<int>();
            //Console.WriteLine("Индекс формы: " + FormIndex);
            Properties = SelectFormView(manager.InspectProject.Forms[FormIndex], listViewObjects);
            RefInfoList = manager.GetListFilesInfo().FindAll(x => x.Type == 'O');
        }

        private void listViewObjects_DoubleClick(object sender, EventArgs e) {
            ListView listview = (ListView)sender;
            if (listViewObjects.SelectedIndices.Count > 0) {
               IDictionary<string, string> prop = Properties[listViewObjects.SelectedIndices[0]];
                listViewProperties.Items.Clear();
                foreach (KeyValuePair<string, string> kvp in prop) {
                    ListViewItem property = new ListViewItem(kvp.Key);
                    property.SubItems.Add(kvp.Value);
                    property.Group = listViewProperties.Groups[0];
                    listViewProperties.Items.Add(property);
                }
                List<SMethod> ObjMethods = methods[listViewObjects.SelectedIndices[0]];
                foreach (SMethod method in ObjMethods) {
                    string[] str = method.Method.Trim('\r', '\n').Split(' ');
                    ListViewItem ObjMethod = new ListViewItem(str[0]);
                    ObjMethod.SubItems.Add(str[1]);
                    ObjMethod.Group = listViewProperties.Groups[1];
                    listViewProperties.Items.Add(ObjMethod);
                }
                listViewRefObjects.Items.Clear();
                if (RefFormList.Count > 0 && RefFormIndex.Count > listViewObjects.SelectedIndices[0] &&
                    RefInfoList != null && RefFormList[listViewObjects.SelectedIndices[0]].Length > 0) {
                    RefProperties = SelectFormView(manager.InspectProject.
                                    OpenForm(RefFormList[listViewObjects.SelectedIndices[0]]),
                                   listViewRefObjects);
                    if(RefFormIndex[listViewObjects.SelectedIndices[0]] >= 0)
                        listViewRefObjects.
                            Items[RefFormIndex[listViewObjects.SelectedIndices[0]]].BackColor =
                            Color.Gray;
                    SCheckElementInfo find = RefInfoList.Find(x => x.EqualNum == FormIndex && x.Type == 'O');
                    richTextBoxRefInfo.Text = "Имя проекта: " + Path.GetFileNameWithoutExtension(find.ProjectName) + "\n" +
                        "Студента: " + find.StudentName + "\n" +
                        "Название формы:" + 
                        Path.GetFileNameWithoutExtension(RefFormList[listViewObjects.SelectedIndices[0]]);
                }
            }
        }

        private List<IDictionary<string, string>> SelectFormView(SForm FormStruct, ListView ListViewFormObjects) {
            List<IDictionary<string, string>> Prop = new List<IDictionary<string, string>>();
            Type type = typeof(SForm);
            var fields = type.GetFields();
            foreach (FieldInfo fi in fields) {
                if (fi.Name != "Name") {
                    list = (List<SObject>)fi.GetValue(FormStruct);
                    for (int i = 0; i < list.Count; i++) {
                        ListViewItem obj = new ListViewItem(list[i].classname);
                        switch (fi.Name) {
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
                        if (list[i].RefFormFullName != null) {
                            RefFormList.Add(list[i].RefFormFullName);
                        } else {
                            RefFormList.Add("");
                        }
                        RefFormIndex.Add(list[i].RefObjectIndex);
                        if (list[i].IsPlagiarism)
                            obj.BackColor = Color.Red;
                        ListViewFormObjects.Items.Add(obj);
                    }
                }
            }
            return Prop;
        }

        private void listViewRefObjects_DoubleClick(object sender, EventArgs e) {
            ListView listview = (ListView)sender;
            if (listViewRefObjects.SelectedIndices.Count > 0) {
                IDictionary<string, string> prop = RefProperties[listViewRefObjects.SelectedIndices[0]];
                listViewRefProperties.Items.Clear();
                foreach (KeyValuePair<string, string> kvp in prop) {
                    ListViewItem property = new ListViewItem(kvp.Key);
                    property.SubItems.Add(kvp.Value);
                    listViewRefProperties.Items.Add(property);
                }
            }
        }

        private void listViewProperties_DoubleClick(object sender, EventArgs e) {
            ListView listview = (ListView)sender;
            if (listViewProperties.SelectedIndices.Count > 0) {
                if (listview.Items[listViewProperties.SelectedIndices[0]].Group == 
                    listview.Groups[1]) {
                    CodeView FormCode = new CodeView();
                    List < SMethod > ObjMethods = methods[listViewObjects.SelectedIndices[0]];
                    //string ObjMethod = ObjMethods[0];
                    SMethod ObjMethod = ObjMethods[listViewProperties.SelectedIndices[0] -
                        listViewProperties.Groups[0].Items.Count];
                    string hash = "";
                    for(int i = 0; i < ObjMethod.Hash.Count; i++) {
                        hash += ObjMethod.Hash[i].ToString() + "\n";
                    }
                    FormCode.richInsText.Text = ObjMethod.Method+"\n\n"+ hash;
                    if(ObjMethod.RefMethod != null) {
                        FormCode.richRefText.Text = ObjMethod.RefMethod;
                    }
                    FormCode.Show();
                }
            }
        }
    }
}
