using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaSearchSystem
{
    internal class ImageMetadata
    {
        public string FilePath { get; set; }
        public string Description { get; set; }
        public double SimilarityScore { get; set; }
    }
}
