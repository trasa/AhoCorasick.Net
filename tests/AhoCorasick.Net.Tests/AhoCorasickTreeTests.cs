﻿using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AhoCorasick.Net.Tests
{
    public class AhoCorasickTreeTests
    {
        [Fact]
        public void CanCreate()
        {
            var sut = new AhoCorasickTree(new[] { "ab", "ab" });
        }

        [Theory]
        [InlineData("d", false)]
        [InlineData("bce", false)]
        [InlineData("bcd", true)]
        [InlineData("abcd", true)]
        public void Contains(string str, bool expected)
        {
            var sut = new AhoCorasickTree(new[] { "ab", "abc", "bcd" });
            Assert.Equal(expected, sut.Contains(str));
        }

        [Fact]
        public void EmptyTrees()
        {
            var tree = new AhoCorasickTree();
            Assert.False(tree.Contains("nothing"));
            Assert.Empty(tree.Search("nothing"));

            tree = new AhoCorasickTree([]);
            Assert.False(tree.Contains("nothing"));
            Assert.Empty(tree.Search("nothing"));
        }

        [Fact]
        public void FindKeywordAndPosition()
        {
            var keywords = new AhoCorasickTree(new[] { "Mozilla", "6.3", "KHTML", "someKeyword" });
            var userAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.99 Safari/537.36";
            var keywordsPositions = keywords.Search(userAgent).ToList();

            Assert.Equal(3, keywordsPositions.Count);
            Assert.Contains(new KeyValuePair<string, int>("Mozilla", 0), keywordsPositions);
            Assert.Contains(new KeyValuePair<string, int>("6.3", 24), keywordsPositions);
            Assert.Contains(new KeyValuePair<string, int>("KHTML", 56), keywordsPositions);
        }

        [Fact]
        public void FindKeywordAndPositionFromWikipedia()
        {
            var keywords = new AhoCorasickTree(new[] { "a", "ab", "bab", "bc", "bca", "c", "caa" });
            var keywordsPositions = keywords.Search("abccab").ToList();
            Assert.Equal(7, keywordsPositions.Count);
            Assert.Contains(new KeyValuePair<string, int>("a", 0), keywordsPositions);
            Assert.Contains(new KeyValuePair<string, int>("ab", 0), keywordsPositions);
            Assert.Contains(new KeyValuePair<string, int>("bc", 1), keywordsPositions);
            Assert.Contains(new KeyValuePair<string, int>("c", 2), keywordsPositions);
            Assert.Contains(new KeyValuePair<string, int>("c", 3), keywordsPositions);
            Assert.Contains(new KeyValuePair<string, int>("a", 4), keywordsPositions);
            Assert.Contains(new KeyValuePair<string, int>("ab", 4), keywordsPositions);
        }

        [Fact]
        public void Performance()
        {
        }
    }
}
