using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Antiplagiat_Projects_VFP9._0 {
    public struct SMethod {
        public string Method;
        public List<int> Hash;
        public string RefMethod;
    }

    public struct SObject {
        public bool IsPlagiarism;
        public string RefFormFullName;
        public string classname;
        public string objname;
        public Dictionary<string,string> properties;
        public int RefObjectIndex;
        public List<SMethod> methods;

        public static bool operator ==(SObject A, SObject B) {
            return (/*A.classname == B.classname &&*/
                A.objname == B.objname &&
                Enumerable.SequenceEqual(A.properties, B.properties));
        }

        public static bool operator !=(SObject A, SObject B) {
            return (/*A.classname != B.classname &&*/
                    A.objname != B.objname &&
                    !Enumerable.SequenceEqual(A.properties, B.properties));
        }
    }

    public struct SForm {
        public string Name;
        public List<SObject> Objects;
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
