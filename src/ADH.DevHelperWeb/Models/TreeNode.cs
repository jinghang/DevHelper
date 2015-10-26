using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace ADH.DevHelperWeb.Models
{

    public class MenuTree
    {
        public IEnumerable<TreeNode> Root { get; set; } 
    }

    /// <summary>
    /// 树形菜单节点
    /// </summary>
    public class TreeNode
    {
        /// <summary>
        /// 节点文本，必选
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 列表树节点上的图标，通常是节点左边的图标。
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 当某个节点被选择后显示的图标，通常是节点左边的图标。
        /// </summary>
        public string selectedIcon { get; set; }
        /// <summary>
        /// 结合全局enableLinks选项为列表树节点指定URL。
        /// </summary>
        public string href { get; set; }
        /// <summary>
        /// 指定列表树的节点是否可选择。设置为false将使节点展开，并且不能被选择。
        /// </summary>
        public bool selectable { get; set; }
        /// <summary>
        /// 一个节点的初始状态。
        /// </summary>
        public NodeState state { get; set; }
        /// <summary>
        /// 节点的前景色，覆盖全局的前景色选项。
        /// </summary>
        public string color { get; set; }
        /// <summary>
        /// 节点的背景色，覆盖全局的背景色选项。
        /// </summary>
        public string backColor { get; set; }
        /// <summary>
        /// 通过结合全局showTags选项来在列表树节点的右边添加额外的信息。
        /// </summary>
        public IEnumerable<String> tags { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public IList nodes { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string path { get; set; }

        public IList children { get { return nodes; } }

        public string attributes { get { return path; } }
    }

    /// <summary>
    /// 节点状态
    /// </summary>
    public class NodeState
    {
        /// <summary>
        /// 指示一个节点是否处于disabled状态。（不是selectable，expandable或checkable）
        /// </summary>
        public bool disabled { get; set; }
        /// <summary>
        /// 指示一个节点是否处于展开状态。
        /// </summary>
        public bool expanded { get; set; }
        /// <summary>
        /// 指示一个节点是否可以被选择。
        /// </summary>
        public bool selected { get; set; }

    }
}