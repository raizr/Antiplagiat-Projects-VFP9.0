using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace Antiplagiat_Projects_VFP9._0 {
    public partial class FormView : Form {
        private CManager manager;
        private List<IDictionary<string, string>> Properties;
        private List<IDictionary<string, string>> RefProperties;
        private List<string> RefFormList;
        public FormView(CManager ptr, int FormIndex) {
            InitializeComponent();
            manager = ptr;
            Properties = new List<IDictionary<string, string>>();
            RefFormList = new List<string>();
            Console.WriteLine("Индекс формы: " + FormIndex);
            Properties = SelectFormView(manager.InspectProject.Forms[FormIndex], listViewObjects);
        }

        private void listViewObjects_DoubleClick(object sender, EventArgs e) {
            ListView listview = (ListView)sender;
            if (listViewObjects.SelectedIndices.Count > 0) {
               IDictionary<string, string> prop = Properties[listViewObjects.SelectedIndices[0]];
                listViewProperties.Items.Clear();
                foreach (KeyValuePair<string, string> kvp in prop) {
                    ListViewItem property = new ListViewItem(kvp.Key);
                    property.SubItems.Add(kvp.Value);
                    listViewProperties.Items.Add(property);
                }
                listViewRefObjects.Items.Clear();
                if (RefFormList.Count > 0)
                    RefProperties = SelectFormView(manager.InspectProject.
                                    OpenForm(RefFormList[listViewObjects.SelectedIndices[0]]),
                                   listViewRefObjects);
            }
        }

        private List<IDictionary<string, string>> SelectFormView(SForm FormStruct, ListView ListViewFormObjects) {
            List<IDictionary<string, string>> Prop = new List<IDictionary<string, string>>();
            Type type = typeof(SForm);
            var fields = type.GetFields();
            foreach (FieldInfo fi in fields) {
                if (fi.Name != "Name") {
                    List<SObject> list = (List<SObject>)fi.GetValue(FormStruct);
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
                        }
                        obj.SubItems.Add(list[i].objname);
                        Prop.Add(list[i].properties);
                        if(list[i].RefFormFullName != null)
                            RefFormList.Add(list[i].RefFormFullName);
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
    }
}
