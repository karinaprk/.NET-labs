using System;
using System.Collections.Generic;
using NUnit.Framework;
using CustomLinkedList;

namespace LinkedListTests;

public class LinkedListTests
{
    [TestCase(new int[] { 1, 2, 3, 4 })]
    [TestCase(new int[] { 10, 2, 3, 2 })]
    [TestCase(new int[] { 1, 2, 1 })]
    [TestCase(new int[] { 0 })]
    public void Constructor_SetsCollectionsCorrect(int[] expected)
    {
        var custom = new CustomLinkedList<int>(expected);
        var actualList = new List<int>();
        
        var current = custom.Head;
        while (current is not null)
        {
            actualList.Add(current.Data);
            current = current.Next;
        }
        
        CollectionAssert.AreEqual(expected, actualList, 
            "Collections must be equal");
    }
    
    [TestCase(-1)]
    [TestCase(2)]
    public void Indexer_GetAndSet_ThrowsArgumentOutOfRangeException(int index)
    {
        var list = new CustomLinkedList<int>(1);
        var expectedException = typeof(ArgumentOutOfRangeException);

        var getException = Assert.Catch(() => list[index].ToString());
        var setException = Assert.Catch(() => { list[index] = 1; });
        
        Assert.Multiple(() =>
        {
            Assert.AreEqual(expectedException, getException.GetType(), 
                "Indexer throws ArgumentOutOfRangeException in case index is out of list range");
            Assert.AreEqual(expectedException, setException.GetType(), 
                "Indexer throws ArgumentOutOfRangeException in case index is out of list range");
        });
    }

    [TestCase(0, 3)]
    [TestCase(2, 2)]
    [TestCase(4, 0)]
    public void Indexer_Get_ReturnsCorrectResult(int index, int expected)
    {
        var list = new List<int>{ 3, 1, 2, 3, 0 };
        var custom = new CustomLinkedList<int>(list);

        int actual = custom[index];
        
        Assert.AreEqual(expected, actual, "Must return the same values under");
    }

    [Test]
    public void Add_NullElement_ShouldThrowsArgumentNullException()
    {
        var list = new CustomLinkedList<string>();
        var expectedException = typeof(ArgumentNullException);

        var actualException = Assert.Catch(() => list.Add(null));
        
        Assert.AreEqual(expectedException, actualException.GetType(), 
            "Must throw ArgumentNullException when argument is null");
    }
    
    [TestCase(new int[] { 1, 2, 3, 4, 5 }, 5)]
    [TestCase(new int[] { 1 }, 1)]
    [TestCase(new int[] { }, 0)]
    public void Add_ShouldReturnCorrectCount(int[] elements, int expectedCount)
    {
        var list = new CustomLinkedList<int>();
        
        foreach (var element in elements)
        {
            list.Add(element);
        }

        Assert.AreEqual(expectedCount, list.Count, 
            "The elements amount must be the same");
    }
    
    [Test]
    public void Remove_NullElement_ShouldThrowsArgumentNullException()
    {
        var list = new CustomLinkedList<string>();
        var expectedException = typeof(ArgumentNullException);

        var actualException = Assert.Catch(() => list.Remove(null));
        
        Assert.AreEqual(expectedException, actualException.GetType(), 
            "Must throw ArgumentNullException when argument is null");
    }
    
    [TestCase(1, new int[] { 2, 3, 4, 5 })]
    [TestCase(2, new int[] { 1, 3, 4, 5 })]
    [TestCase(4, new int[] { 1, 2, 3, 5 })]
    public void Remove_ShouldRemovesElementCorrect(int toRemove, int[] expected)
    {
        var list = new int[] { 1, 2, 3, 4, 5 };
        var custom = new CustomLinkedList<int>(list);
        var actualList = new List<int>();
        
        custom.Remove(toRemove);
        
        var current = custom.Head;
        while (current is not null)
        {
            actualList.Add(current.Data);
            current = current.Next;
        }

        Assert.Multiple(() =>
        {
            Assert.AreEqual(4, custom.Count, 
                "The elements amount must be the same");
            CollectionAssert.AreEqual(expected, actualList, 
                "Collections must be equal");
        });
    }
    
    [TestCase(3, true)]
    [TestCase(1, true)]
    [TestCase(0, false)]
    public void Contains_ShouldRemovesElementCorrect(int toFind, bool expected)
    {
        var list = new int[] {1, 2, 3, 4, 5};
        var custom = new CustomLinkedList<int>(list);

        var actual = custom.Contains(toFind);
        
        Assert.AreEqual(expected, actual, 
            "Contains must return true when element is in collection");
    }
    
    [TestCase(0)]
    [TestCase(6)]
    [TestCase(9)]
    public void IndexOf_ShouldReturnsMinusOneWhenNotFound(int toFind)
    {
        int expected = -1;

        var list = new int[] { 1, 2, 3, 4, 5 };
        var custom = new CustomLinkedList<int>(list);

        var actual = custom.IndexOf(toFind);
        
        Assert.AreEqual(expected, actual, 
            "IndexOf must return -1 when the collection does not contain the element");
    }
    
    [TestCase(3, 2)]
    [TestCase(1, 0)]
    [TestCase(5, 4)]
    public void IndexOf_ShouldReturnsCorrectElement(int element, int expected)
    {
        var list = new int[] { 1, 2, 3, 4, 5 };
        var custom = new CustomLinkedList<int>(list);

        var actual = custom.IndexOf(element);
        
        Assert.AreEqual(expected, actual, 
            "IndexOf must return correct index of element");
    }
    
    [Test]
    public void GetEnumerator_OfArrayAndCustomList_ShouldHaveEqualElements()
    {
        var array = new[] { 0, 1, 2, 3, 4, 5 };
        var custom = new CustomLinkedList<int>(array);

        var listEnumerator = array.GetEnumerator();
        var customListEnumerator = custom.GetEnumerator();
        
        while (listEnumerator.MoveNext() && customListEnumerator.MoveNext())
        {
            Assert.AreEqual(listEnumerator.Current, customListEnumerator.Current,
                message: "GetEnumerator must returns the same elements as GetEnumerator of array");
        }
    }
}