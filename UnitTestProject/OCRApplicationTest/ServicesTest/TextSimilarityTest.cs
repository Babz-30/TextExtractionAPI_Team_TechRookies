using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCRApplication.Services;
using System;
using System.Collections.Generic;

namespace OCRApplicationTest.ServicesTest
{
    /// <summary>
    /// Test class for TextSimilarity, verifying the correctness of  Cosine Similarity calculations
    /// </summary>
    [TestClass]
    public class TextSimilarityTest
    {
        /// <summary>
        /// Tests that Compute Cosine Similarity return 1.0 for identical vectors.
        /// </summary>
        [TestMethod]
        public void ComputeCosineSimilarity_ValidVectors_ReturnsCorrectValue()
        {
            List<double> vectorA = [1, 2, 3];
            List<double> vectorB = [1, 2, 3];
            double similarity = TextSimilarity.ComputeCosineSimilarity(vectorA, vectorB);
            Assert.AreEqual(1.0, similarity, 0.0001, "Cosine similarity should be 1 for identical vectors.");
        }

        /// <summary>
        /// Tests that Compute Cosine Similarity throws an exception when given vectors of different lengths.
        /// </summary>
        [TestMethod]
        public void ComputeCosineSimilarityDifferentLengthVectorsThrowsException()
        {
            List<double> vectorA = [1, 2, 3];
            List<double> vectorB = [1, 2];
            Assert.ThrowsExactly<ArgumentException>(() => TextSimilarity.ComputeCosineSimilarity(vectorA, vectorB));
        }

        /// <summary>
        /// Tests that Compute Cosine Similarity returns 0.0 for orthogonal vectors which have no similarity
        /// </summary>
        [TestMethod]
        public void ComputeCosineSimilarity_OrthogonalVectors_ReturnsZero()
        {
            List<double> vectorA = [1, 0];
            List<double> vectorB = [0, 1];
            double similarity = TextSimilarity.ComputeCosineSimilarity(vectorA, vectorB);
            Assert.AreEqual(0.0, similarity, 0.0001, "Cosine similarity should be 0 for orthogonal vectors.");
        }
    }
}
