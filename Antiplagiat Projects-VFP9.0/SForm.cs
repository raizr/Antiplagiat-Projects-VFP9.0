using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Antiplagiat_Projects_VFP9._0 {
    public struct SObject {
        public bool IsPlagiarism;
        public string RefFormFullName;
        public string classname;
        public string objname;
        public Dictionary<string,string> properties;
        public string methods;

        public static bool operator ==(SObject A, SObject B) {
            return (A.classname == B.classname &&
                A.objname == B.objname &&
                Enumerable.SequenceEqual(A.properties, B.properties));
        }

        public static bool operator !=(SObject A, SObject B) {
            return (A.classname != B.classname &&
                    A.objname != B.objname &&
                    !Enumerable.SequenceEqual(A.properties, B.properties));
        }
    }

    public struct SForm {
        public string Name;
        public List<SObject> form;
        public List<SObject> commandbutton;
        public List<SObject> header;
        public List<SObject> textbox;
        public List<SObject> grid;
        public List<SObject> label;
        public List<SObject> pageframe;
        public List<SObject> editbox;
        public List<SObject> spinner;
        public List<SObject> optiongroup;
        public List<SObject> checkbox;
        public List<SObject> combobox;

        /*public static bool operator ==(SForm A, SForm B) {
            return (A.Name == B.Name &&
            new HashSet<SObject>(A.form, new SObjectEqualityComparer()).SetEquals(B.form) &&
            new HashSet<SObject>(A.commandbutton, new SObjectEqualityComparer()).SetEquals(B.commandbutton) &&
            new HashSet<SObject>(A.header, new SObjectEqualityComparer()).SetEquals(B.header) &&
            new HashSet<SObject>(A.textbox, new SObjectEqualityComparer()).SetEquals(B.textbox) &&
            new HashSet<SObject>(A.grid, new SObjectEqualityComparer()).SetEquals(B.grid) &&
            new HashSet<SObject>(A.label, new SObjectEqualityComparer()).SetEquals(B.label) &&
            new HashSet<SObject>(A.pageframe, new SObjectEqualityComparer()).SetEquals(B.pageframe) &&
            new HashSet<SObject>(A.editbox, new SObjectEqualityComparer()).SetEquals(B.editbox) &&
            new HashSet<SObject>(A.spinner, new SObjectEqualityComparer()).SetEquals(B.spinner));
        }

        public static bool operator !=(SForm A, SForm B) {
            return (A.Name != B.Name &&
            !new HashSet<SObject>(A.form, new SObjectEqualityComparer()).SetEquals(B.form) &&
            !new HashSet<SObject>(A.commandbutton, new SObjectEqualityComparer()).SetEquals(B.commandbutton) &&
            !new HashSet<SObject>(A.header, new SObjectEqualityComparer()).SetEquals(B.header) &&
            !new HashSet<SObject>(A.textbox, new SObjectEqualityComparer()).SetEquals(B.textbox) &&
            !new HashSet<SObject>(A.grid, new SObjectEqualityComparer()).SetEquals(B.grid) &&
            !new HashSet<SObject>(A.label, new SObjectEqualityComparer()).SetEquals(B.label) &&
            !new HashSet<SObject>(A.pageframe, new SObjectEqualityComparer()).SetEquals(B.pageframe) &&
            !new HashSet<SObject>(A.editbox, new SObjectEqualityComparer()).SetEquals(B.editbox) &&
            !new HashSet<SObject>(A.spinner, new SObjectEqualityComparer()).SetEquals(B.spinner));
        }*/
    }

}
