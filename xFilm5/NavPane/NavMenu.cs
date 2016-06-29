using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Xml;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Common.Resources;
using Gizmox.WebGUI.Forms;

namespace xFilm5.NavPane
{
    public class NavMenu
    {
        public static void FillNavTree(string filter, TreeNodeCollection oNav)
        {
            XmlDocument oXmlDoc = new XmlDocument();
            string _Source = System.Web.HttpContext.Current.Server.MapPath(Controls.Utility.User.GetUserMenu(xFilm5.DAL.Common.Config.CurrentUserId));

            try
            {
                oXmlDoc.Load(_Source);
            }
            catch (Exception e)
            {
                throw e;
            }

            XmlNodeList oNodeList = oXmlDoc.DocumentElement.ChildNodes;
            foreach (XmlNode oNode in oNodeList)
            {
                XmlNode oCurNode = oNode;

                if (oCurNode.HasChildNodes && oCurNode.Name.ToLower() == filter)
                {
                    FillTreeMenu(oNode, oNav);
                }
            }
        }
        // 2007.10.23 paulus: I use FillTree_ to skips the first node
        private static void FillTreeMenu(XmlNode node, TreeNodeCollection parentnode)
        {
            // Add all the children of the current node to the treeview
            foreach (XmlNode tmpchildnode in node.ChildNodes)
            {
                FillTree(tmpchildnode, parentnode);
            }
        }

        private static void FillTree(XmlNode node, TreeNodeCollection parentnode)
        {
            TreeNodeCollection tmpNodes = AddNodeToTree(node, parentnode);

            // Add all the children of the current node to the treeview
            foreach (XmlNode tmpchildnode in node.ChildNodes)
            {
                FillTree(tmpchildnode, tmpNodes);
            }
        }

        private static TreeNodeCollection AddNodeToTree(XmlNode node, TreeNodeCollection parentnode)
        {
            TreeNode newchildnode = CreateTreeNodeFromXmlNode(node);
            // if nothing to add, return the parent item
            if (newchildnode == null) return parentnode;
            // add the newly created tree node to its parent
            if (parentnode != null) parentnode.Add(newchildnode);
            return newchildnode.Nodes;
        }

        private static Gizmox.WebGUI.Forms.TreeNode CreateTreeNodeFromXmlNode(XmlNode node)
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(xFilm5.DAL.Common.Config.CurrentWordDict, xFilm5.DAL.Common.Config.CurrentLanguageId);

            TreeNode tmptreenode = new TreeNode();
            if ((node.HasChildNodes) && (node.FirstChild.Value != null))
            {
                tmptreenode = new TreeNode(node.Name);
                TreeNode tmptreenode2 = new TreeNode(node.FirstChild.Value);
                tmptreenode.Nodes.Add(tmptreenode2);
            }
            else if (node.NodeType != XmlNodeType.CDATA)
            {
                if (node.HasChildNodes)
                {
                    Font font = new Font("Tahoma", 11, FontStyle.Bold, GraphicsUnit.Pixel);

                    TreeNode oNode = new TreeNode();

                    oNode.Label = oDict.GetWord(node.Attributes["Caption"].Value); 
                    oNode.NodeFont = font;
                    oNode.IsExpanded    = false;

                    tmptreenode = oNode;
                }
                else
                {
                    Font font = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);

                    TreeNode oNode = new TreeNode();

                    oNode.Label = oDict.GetWord(node.Attributes["Caption"].Value.Replace("'", ""));
                    oNode.Tag = node.Attributes["Tag"].Value;
                    oNode.Image     = new IconResourceHandle(node.Attributes["ImageUrl"].Value);
                    oNode.NodeFont  = font;

                    tmptreenode = oNode;
                }
            }
            return tmptreenode;
        }
    }
}
