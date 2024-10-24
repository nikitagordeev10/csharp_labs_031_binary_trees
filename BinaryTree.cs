using System;
using System.Collections;
using System.Collections.Generic;

namespace Generics.BinaryTrees { // пространство имен 

    public class BinaryTree<T> : IEnumerable<T> where T : IComparable<T> { // класс с параметром типа T

        public BinaryTree<T> Left; // поле класса, левая ветвь дерева
        public BinaryTree<T> Right; // поле класса, правая ветвь дерева
        public T Value; // значение текущего узла
        private bool isValueSet; //  добавлен ли элемент в дерево 

        public BinaryTree() { // объявление конструктора класса BinaryTree
            this.Value = default(T);   // значения по умолчанию для всех типов значений
            this.isValueSet = false;   // флаг для указания задано ли значение Value
        }

        public void Add(T element) { // метод, добавляющий элемент к дереву
            if (!isValueSet) {
                this.Value = element;   // устанавливаем значение узла
                isValueSet = true;      // сообщаем, что значение установлено
            } else if (this.Value.CompareTo(element) >= 0) {    // сравниваем значение текущего узла и добавляемого значения -  текущее значение равно или больше добавляемого элемента - добавляем его в левую ветвь
                if (this.Left == null) this.Left = new BinaryTree<T>();   // левой ветви еще не существует, создадим новое дерево 
                this.Left.Add(element);    // рекурсивно добавляем элемент в левую ветвь
            } else {     // текущее значение меньше или больше добавляемого элемента, добавляем новый элемент в правую ветвь
                if (this.Right == null) this.Right = new BinaryTree<T>();  // правой ветви еще не существует, создадим новое дерево 
                this.Right.Add(element);   // рекурсивно добавляем элемент в правую ветвь
            }
        }

        public IEnumerator<T> GetEnumerator() { // метод перечислитель
            if (!isValueSet) {    // значение не установлено, завершаем работу
                yield break;
            }

            if (Left != null) { // добавляем все элементы в левой ветви к перечислителю
                foreach (var value in Left) // перебираем элементы в левой ветви
                    yield return value;  // добавляем их в перечислитель
            }
            yield return Value;     // добавляем значение текущего узла в перечислитель

            if (Right != null) { // добавляем все элементы в правой ветви к перечислителю
                foreach (var value in Right)  // перебираем все элементы в правой ветви
                    yield return value;       // добавляем их в перечислитель
            }
        }

        IEnumerator IEnumerable.GetEnumerator() { // метод GetEnumerator из непараметризованного интерфейса IEnumerable
            return GetEnumerator();
        }
    }

    public class BinaryTree { // объявляется класс BinaryTree       
        public static BinaryTree<T> Create<T>(params T[] values) where T : IComparable<T> { // метод создает объект BinaryTree<T> из массива values
            var binaryTree = new BinaryTree<T>();   // создаем экземпляр двоичного дерева
            foreach (var value in values) {
                binaryTree.Add(value);  // добавляем каждый элемент массива в дерево
            }
            return binaryTree;   // возвращаем новый экземпляр двоичного дерева
        }
    }
}