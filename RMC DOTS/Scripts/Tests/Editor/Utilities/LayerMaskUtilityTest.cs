using NUnit.Framework;
using RMC.Core.Utilities;
using UnityEngine;

namespace RMC.DOTS.Utilities
{
    [Category(nameof (Utilities))]
    [TestFixture]
    public class LayerMaskUtilityTest 
    {
        [SetUp]
        public void Setup()
        {
        }
        
        
        /// <summary>
        /// Only check the uneditable Unity built-in layers.
        /// Do not check any custom layers
        /// </summary>
        [Test]
        public void AssertLayerMask_IsTrue_WhenBuiltInLayers()
        {
            // Arrange
            
            // Act

            // Assert
        }

            
        [Test]
        public void LayerMaskContainsLayer_ResultIsTrue_WhenAddLayer()
        {
            // Arrange
            int layerIndex = 1;
            LayerMask layerMask = new LayerMask();
            layerMask = LayerMaskUtility.AddLayer(layerMask, layerIndex);
            
            // Act
            var result = LayerMaskUtility.LayerMaskContainsLayer(layerMask, layerIndex);

            // Assert
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void LayerMaskContainsLayer_ResultIsFalse_WhenAddLayerRemoveLayer()
        {
            // Arrange
            int layerIndex = 1;
            LayerMask layerMask = new LayerMask();
            layerMask = LayerMaskUtility.AddLayer(layerMask, layerIndex);
            layerMask = LayerMaskUtility.RemoveLayer(layerMask, layerIndex);
            
            // Act
            var result = LayerMaskUtility.LayerMaskContainsLayer(layerMask, layerIndex);

            // Assert
            Assert.That(result, Is.False);
        }

        
        [Test]
        public void LayerMaskContainsLayer_ResultIsFalse_WhenNotAddLayer()
        {
            // Arrange
            int layerIndex = 1;
            LayerMask layerMask = new LayerMask();
            
            // Act
            var result = LayerMaskUtility.LayerMaskContainsLayer(layerMask, layerIndex);

            // Assert
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void LayerMaskContainsLayerMask_ResultIsTrue_WhenEmptyLayersr()
        {
            // Arrange
            LayerMask layerMask1 = new LayerMask();
            LayerMask layerMask2 = new LayerMask();
            
            // Act
            var result = LayerMaskUtility.LayerMaskContainsLayerMask(layerMask1, layerMask2);

            // Assert
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void LayerMaskContainsLayerMask_ResultIsTrue_WhenSameLayerMask()
        {
            // Arrange
            LayerMask layerMask1 = new LayerMask();
            
            // Act
            var result = LayerMaskUtility.LayerMaskContainsLayerMask(layerMask1, layerMask1);

            // Assert
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void LayerMaskContainsLayerMask_ResultIsTrue_WhenAddLayer()
        {
            // Arrange
            int layerIndex1 = 1;
            LayerMask layerMask1 = new LayerMask();
            layerMask1 = LayerMaskUtility.AddLayer(layerMask1, layerIndex1);
            
            int layerIndex2 = 1;
            LayerMask layerMask2 = new LayerMask();
            layerMask2 = LayerMaskUtility.AddLayer(layerMask2, layerIndex2);
            
            // Act
            var result = LayerMaskUtility.LayerMaskContainsLayerMask(layerMask1, layerMask2);

            // Assert
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void LayerMaskContainsLayerMask_ResultIsTrue_WhenBigContainsSmall()
        {
            // Arrange
            int layerIndex1 = 1;
            int layerIndex2 = 2;
            int layerIndex3 = 3;
            
            LayerMask bigLayerMask = new LayerMask();
            bigLayerMask = LayerMaskUtility.AddLayer(bigLayerMask, layerIndex1);
            bigLayerMask = LayerMaskUtility.AddLayer(bigLayerMask, layerIndex2);
            bigLayerMask = LayerMaskUtility.AddLayer(bigLayerMask, layerIndex3);
            
            LayerMask smallLayerMask = new LayerMask();
            smallLayerMask = LayerMaskUtility.AddLayer(smallLayerMask, layerIndex1);
            
            // Act
            var result = LayerMaskUtility.LayerMaskContainsLayerMask(bigLayerMask, smallLayerMask);

            // Assert
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void LayerMaskContainsLayerMask_ResultIsFalse_WhenSmallContainsBig()
        {
            // Arrange
            int layerIndex1 = 1;
            int layerIndex2 = 2;
            int layerIndex3 = 3;
            
            LayerMask bigLayerMask = new LayerMask();
            bigLayerMask = LayerMaskUtility.AddLayer(bigLayerMask, layerIndex1);
            bigLayerMask = LayerMaskUtility.AddLayer(bigLayerMask, layerIndex2);
            bigLayerMask = LayerMaskUtility.AddLayer(bigLayerMask, layerIndex3);
            
            LayerMask smallLayerMask = new LayerMask();
            smallLayerMask = LayerMaskUtility.AddLayer(smallLayerMask, layerIndex1);
            
            // Act
            var result = LayerMaskUtility.LayerMaskContainsLayerMask(smallLayerMask, bigLayerMask);

            // Assert
            Assert.That(result, Is.False);
        }
    }
}
