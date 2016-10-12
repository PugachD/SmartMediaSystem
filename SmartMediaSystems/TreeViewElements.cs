using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartMediaSystems
{
    class TreeViewElements: IEquatable<TreeViewElements>
    {
        public int ID { get; set; }
        public int Parent_ID { get; set; }
        public string Name { get; set; }
        public int CID { get; set; }

        public bool Equals(TreeViewElements tree)
        {
            //Check whether the compared object is null. 
            if (Object.ReferenceEquals(tree, null)) return false;

            //Check whether the compared object references the same data. 
            if (Object.ReferenceEquals(this, tree)) return true;

            //Check whether the trees' properties are equal. 
            return CID.Equals(tree.CID) && Name.Equals(tree.Name) && Parent_ID.Equals(tree.Parent_ID);
        }

        public override int GetHashCode()
        {

            //Get hash code for the Name field if it is not null. 
            int hashTreeName = Name == null ? 0 : Name.GetHashCode();

            //Get hash code for the Parent_ID field. 
            int hashTreeCode = Parent_ID.GetHashCode();

            //Get hash code for the CID field. 
            int hashTreeCID = CID.GetHashCode();

            //Calculate the hash code for the tree. 
            return hashTreeName ^ hashTreeCode ^ hashTreeCID;
        }

    }
}
