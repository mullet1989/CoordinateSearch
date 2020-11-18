using System.Collections.Generic;
using System.IO;
using System.Linq;
using KdTree;
using KdTree.Math;
using KTrie;

namespace CoordinateSearchApi.Services
{
    public class SearchService
    {
        private KdTree<float, Place> _tree = new KdTree<float, Place>(2, new GeoMath());
        private StringTrie<IList<Place>> _trie = new StringTrie<IList<Place>>(new LowerCaseComparer());

        public SearchService()
        {
            var lines = File.ReadAllLines("./data/cities500.txt");

            for (int i = 1; i < lines.Length - 1; i++)
            {
                var split = lines[i].Split('\t');

                var place = new Place
                {
                    Id = int.Parse(split[0]),
                    Name = split[2],
                    Latitude = float.Parse(split[4]),
                    Longitude = float.Parse(split[5]),
                };

                _tree.Add(new[] { float.Parse(split[4]), float.Parse(split[5]) }, place);

                // if already contained
                if (_trie.ContainsKey(split[2]))
                {
                    var places = _trie[split[2]];
                    places.Add(place);
                }
                else
                {
                    _trie.Add(split[2], new List<Place> { place });
                }
            }
        }

        public IEnumerable<Place> Search(float latitude, float longitude)
            => _tree.GetNearestNeighbours(new[] { latitude, longitude }, 10).Select(x => x.Value);

        public IEnumerable<Place> Query(string query)
            => _trie.GetByPrefix(query).SelectMany(x => x.Value);

        public IEnumerable<Place> Point(float latitude, float longitude, float radius)
            => _tree.RadialSearch(new float[] { latitude, longitude }, radius).Select(x => x.Value);
    }
}