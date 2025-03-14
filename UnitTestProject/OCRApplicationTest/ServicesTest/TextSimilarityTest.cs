using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCRApplication.Services;
using System;
using System.Collections.Generic;

namespace OCRApplicationTest
{
    [TestClass]
    public class TextSimilarityTest
    {
        [TestMethod]
        public void ComputeCosineSimilarity_ValidVectors_ReturnsCorrectValue()
        {
            List<double> vectorA = new List<double> { 1, 2, 3 };
            List<double> vectorB = new List<double> { 1, 2, 3 };
            double similarity = TextSimilarity.ComputeCosineSimilarity(vectorA, vectorB);
            Assert.AreEqual(1.0, similarity, 0.0001, "Cosine similarity should be 1 for identical vectors.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ComputeCosineSimilarity_DifferentLengthVectors_ThrowsException()
        {
            List<double> vectorA = new List<double> { 1, 2, 3 };
            List<double> vectorB = new List<double> { 1, 2 };
            TextSimilarity.ComputeCosineSimilarity(vectorA, vectorB);
        }

        [TestMethod]
        public void ComputeCosineSimilarity_OrthogonalVectors_ReturnsZero()
        {
            List<double> vectorA = new List<double> { 1, 0 };
            List<double> vectorB = new List<double> { 0, 1 };
            double similarity = TextSimilarity.ComputeCosineSimilarity(vectorA, vectorB);
            Assert.AreEqual(0.0, similarity, 0.0001, "Cosine similarity should be 0 for orthogonal vectors.");
        }
    }
}
