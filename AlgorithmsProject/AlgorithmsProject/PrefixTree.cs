using System;
using System.Collections.Generic;

namespace AlgorithmsProject
{
    public class PrefixTree<T>
    {
        public PrefixTreeNode<T> _root = new PrefixTreeNode<T>(null, "");

        public void Add(string key, T value)
        {
            PrefixTreeNode<T> node = GetNode(key);

            if (node != null)
            {
                node.SetValue(value);
                return;
            }

            string additiveKey = "";
            node = _root;

            for (int i = 0; i < key.Length; i++)
            {
                additiveKey += key[i];
                node = Add(additiveKey, _root);
            }

            node.SetValue(value);
        }

        private PrefixTreeNode<T> Add(string key, PrefixTreeNode<T> node)
        {
            if (node.key == key)
                return node;

            if (node.childs == null || node.childs.Count == 0)
            {
                node.AddChild(new PrefixTreeNode<T>(node, key));
                return node.childs[0];
            }

            for (int i = 0; i < node.childs.Count; i++)
                if (key.StartsWith(node.childs[i].key))
                    return Add(key, node.childs[i]);

            node.AddChild(new PrefixTreeNode<T>(node, key));
            return node.childs[node.childs.Count - 1];
        }


        public void Remove(string key)
        {
            PrefixTreeNode<T> node = GetNode(key);

            if (node == null || node == _root)
                return;

            node.DeleteValue();

            if (node.childs != null && node.childs.Count > 0)
                return;

            while (node.parent != _root && !node.hasValue)
            {
                if (node.childs != null && node.childs.Count > 0)
                    break;

                node.parent.childs.Remove(node);
                node = node.parent;
            }
        }


        public bool ContainsKey(string key)
        {
            return GetNode(key) != null;
        }

        public bool ContainsValue(string key)
        {
            PrefixTreeNode<T> node = GetNode(key);

            if (node == null)
                return false;

            return node.hasValue;
        }


        public PrefixTreeNode<T> GetNode(string key)
        {
            return GetNode(key, _root);
        }

        public PrefixTreeNode<T> GetNode(string key, PrefixTreeNode<T> node)
        {
            if (node.key == key)
                return node;

            if (node.childs == null || node.childs.Count == 0)
                return null;

            for (int i = 0; i < node.childs.Count; i++)
                if (key.StartsWith(node.childs[i].key))
                    return GetNode(key, node.childs[i]);

            return null;
        }


        public T GetValue(string key)
        {
            if (!ContainsKey(key))
                throw new ArgumentException("Слово: '" + key + "' не существует в данном словаре. Воспользуйтесь командой '1', если хотите его добавить.");

            PrefixTreeNode<T> node = GetNode(key);

            if (!node.hasValue)
                throw new Exception("У слова '" + key + "' нет перевода в данном словаре, поищите в других словарях или добавьте сами");

            return node.value;
        }
    }

    public class PrefixTreeNode<T>
    {
        public string key { get; private set; }
        public T value { get; private set; }
        public bool hasValue { get; private set; }
        public PrefixTreeNode<T> parent { get; private set; }
        public List<PrefixTreeNode<T>> childs { get; private set; }

        public PrefixTreeNode(PrefixTreeNode<T> parent, string key)
        {
            this.parent = parent;
            this.key = key;
        }

        public void SetValue(T value)
        {
            this.value = value;
            hasValue = true;
        }

        public void DeleteValue()
        {
            hasValue = false;
            value = default(T);
        }

        public void AddChild(PrefixTreeNode<T> node)
        {
            if (childs == null)
                childs = new List<PrefixTreeNode<T>>();

            if (!childs.Contains(node))
                childs.Add(node);
        }
    }
}
