using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Erp.BackOffice.Staff.Models
{
    public class TreeNode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public object Tag { get; set; }
        public string Image { get; set; }
        public string TableName { get; set; }
        public string TypeName { get; set; }
        public string GuidId { get; set; }

        public List<TreeNode> ChildNode { get; set; }
        public TreeNode()
        {
            ChildNode = new List<TreeNode>();
        }
        public TreeNode(string name)
        {
            ChildNode = new List<TreeNode>();
            this.Name = name;
        }
        //Popup send
        public bool Disable { get; set; }
        public int? ParentId { get; internal set; }
    }
}