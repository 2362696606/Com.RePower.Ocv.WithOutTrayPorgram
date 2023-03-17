using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Com.RePower.Ocv.Ui.UiBase.ViewModels
{
    public class ObservableLinkedList<T> : LinkedList<T>, INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        #region Ctor

        public ObservableLinkedList() : base() { }
        public ObservableLinkedList(IEnumerable<T> collection) : base() { }
        #endregion

        //
        // 摘要:
        //     Adds the specified new node after the specified existing node in the System.Collections.Generic.LinkedList`1.
        //
        // 参数:
        //   node:
        //     The System.Collections.Generic.LinkedListNode`1 after which to insert newNode.
        //
        //   newNode:
        //     The new System.Collections.Generic.LinkedListNode`1 to add to the System.Collections.Generic.LinkedList`1.
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     node is null. -or- newNode is null.
        //
        //   T:System.InvalidOperationException:
        //     node is not in the current System.Collections.Generic.LinkedList`1. -or- newNode
        //     belongs to another System.Collections.Generic.LinkedList`1.
        public new void AddAfter(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newNode, node));
            base.AddAfter(node, newNode);
        }
        //
        // 摘要:
        //     Adds a new node containing the specified value after the specified existing node
        //     in the System.Collections.Generic.LinkedList`1.
        //
        // 参数:
        //   node:
        //     The System.Collections.Generic.LinkedListNode`1 after which to insert a new System.Collections.Generic.LinkedListNode`1
        //     containing value.
        //
        //   value:
        //     The value to add to the System.Collections.Generic.LinkedList`1.
        //
        // 返回结果:
        //     The new System.Collections.Generic.LinkedListNode`1 containing value.
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     node is null.
        //
        //   T:System.InvalidOperationException:
        //     node is not in the current System.Collections.Generic.LinkedList`1.
        public new LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value, node));
            return base.AddAfter(node, value);
        }
        //
        // 摘要:
        //     Adds the specified new node before the specified existing node in the System.Collections.Generic.LinkedList`1.
        //
        // 参数:
        //   node:
        //     The System.Collections.Generic.LinkedListNode`1 before which to insert newNode.
        //
        //   newNode:
        //     The new System.Collections.Generic.LinkedListNode`1 to add to the System.Collections.Generic.LinkedList`1.
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     node is null. -or- newNode is null.
        //
        //   T:System.InvalidOperationException:
        //     node is not in the current System.Collections.Generic.LinkedList`1. -or- newNode
        //     belongs to another System.Collections.Generic.LinkedList`1.
        public new void AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newNode, node));
            base.AddBefore(node, newNode);
        }
        //
        // 摘要:
        //     Adds a new node containing the specified value before the specified existing
        //     node in the System.Collections.Generic.LinkedList`1.
        //
        // 参数:
        //   node:
        //     The System.Collections.Generic.LinkedListNode`1 before which to insert a new
        //     System.Collections.Generic.LinkedListNode`1 containing value.
        //
        //   value:
        //     The value to add to the System.Collections.Generic.LinkedList`1.
        //
        // 返回结果:
        //     The new System.Collections.Generic.LinkedListNode`1 containing value.
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     node is null.
        //
        //   T:System.InvalidOperationException:
        //     node is not in the current System.Collections.Generic.LinkedList`1.
        public new LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value, node));
            return base.AddBefore(node, value);
        }
        //
        // 摘要:
        //     Adds the specified new node at the start of the System.Collections.Generic.LinkedList`1.
        //
        // 参数:
        //   node:
        //     The new System.Collections.Generic.LinkedListNode`1 to add at the start of the
        //     System.Collections.Generic.LinkedList`1.
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     node is null.
        //
        //   T:System.InvalidOperationException:
        //     node belongs to another System.Collections.Generic.LinkedList`1.
        public new void AddFirst(LinkedListNode<T> node)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, node));
            base.AddFirst(node);
        }
        //
        // 摘要:
        //     Adds a new node containing the specified value at the start of the System.Collections.Generic.LinkedList`1.
        //
        // 参数:
        //   value:
        //     The value to add at the start of the System.Collections.Generic.LinkedList`1.
        //
        // 返回结果:
        //     The new System.Collections.Generic.LinkedListNode`1 containing value.
        public new LinkedListNode<T> AddFirst(T value)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value, 0));
            return base.AddFirst(value);
        }
        //
        // 摘要:
        //     Adds the specified new node at the end of the System.Collections.Generic.LinkedList`1.
        //
        // 参数:
        //   node:
        //     The new System.Collections.Generic.LinkedListNode`1 to add at the end of the
        //     System.Collections.Generic.LinkedList`1.
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     node is null.
        //
        //   T:System.InvalidOperationException:
        //     node belongs to another System.Collections.Generic.LinkedList`1.
        public new void AddLast(LinkedListNode<T> node)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, node));
            base.AddLast(node);
        }
        //
        // 摘要:
        //     Adds a new node containing the specified value at the end of the System.Collections.Generic.LinkedList`1.
        //
        // 参数:
        //   value:
        //     The value to add at the end of the System.Collections.Generic.LinkedList`1.
        //
        // 返回结果:
        //     The new System.Collections.Generic.LinkedListNode`1 containing value.
        public new LinkedListNode<T> AddLast(T value)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value));
            return base.AddLast(value); 
        }
        //
        // 摘要:
        //     Removes all nodes from the System.Collections.Generic.LinkedList`1.
        public new void Clear()
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            base.Clear();
        }

        //
        // 摘要:
        //     Removes the specified node from the System.Collections.Generic.LinkedList`1.
        //
        // 参数:
        //   node:
        //     The System.Collections.Generic.LinkedListNode`1 to remove from the System.Collections.Generic.LinkedList`1.
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     node is null.
        //
        //   T:System.InvalidOperationException:
        //     node is not in the current System.Collections.Generic.LinkedList`1.
        public new void Remove(LinkedListNode<T> node)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset, node));
            base.Remove(node);
        }
        //
        // 摘要:
        //     Removes the first occurrence of the specified value from the System.Collections.Generic.LinkedList`1.
        //
        // 参数:
        //   value:
        //     The value to remove from the System.Collections.Generic.LinkedList`1.
        //
        // 返回结果:
        //     true if the element containing value is successfully removed; otherwise, false.
        //     This method also returns false if value was not found in the original System.Collections.Generic.LinkedList`1.
        public new bool Remove(T value)
        {
            if(base.Remove(value))
            {
                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset, value));
                return true;
            }
            return false;
        }
        //
        // 摘要:
        //     Removes the node at the start of the System.Collections.Generic.LinkedList`1.
        //
        // 异常:
        //   T:System.InvalidOperationException:
        //     The System.Collections.Generic.LinkedList`1 is empty.
        public new void RemoveFirst()
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, null, 0));
            base.RemoveFirst();
        }
        //
        // 摘要:
        //     Removes the node at the end of the System.Collections.Generic.LinkedList`1.
        //
        // 异常:
        //   T:System.InvalidOperationException:
        //     The System.Collections.Generic.LinkedList`1 is empty.
        public new void RemoveLast()
        {
            var item = this.Last();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, this.Count - 1));
            base.RemoveLast();
        }

    }
}
