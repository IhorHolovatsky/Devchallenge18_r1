using System.Collections.Generic;
using System.Linq;
using BoxCutting.Core.Interfaces;
using BoxCutting.Core.Models;
using NetTopologySuite.Geometries;

namespace BoxCutting.Core.Packers
{
    /// <summary>
    /// BinaryTreePacker identifies positions for BoxCuttings
    /// </summary>
    public class BinaryTreePacker : IPacker
    {
        public List<BoxPackingResult> PackBoxCuttings(Sheet sheet, List<IBoxCutting> boxCuttings)
        {
            var packResults = new List<BoxPackingResult>();

            // Represents root node of binary tree
            var root = new Node
            {
                X = 0,
                Y = 0,
                Width = sheet.Width,
                Height = sheet.Length
            };

            // TODO: since we are growing "right" then "up", double-check which sorting will be best for us
            if (sheet.Width > sheet.Length)
            {
                boxCuttings = boxCuttings
                    // Sort by efficiency for placing most efficient BoxCuttings first
                    .OrderByDescending(x => x.Efficiency)
                    .ThenByDescending(x => x.Height)
                    .ToList();
            }
            else
            {
                boxCuttings = boxCuttings
                    // Sort by efficiency for placing most efficient BoxCuttings first
                    .OrderByDescending(x => x.Efficiency)
                    .ThenByDescending(x => x.Width)
                    .ToList();
            }


            for (var i = 0; i < boxCuttings.Count;)
            {
                var boxCutting = boxCuttings[i];

                Node node;

                if (boxCutting is IBoxCuttingPattern pattern)
                {
                    // Get nearest node of given Width and Height
                    // TODO: do we need to know on which direction pattern build BoxCuttings (for better results)
                    node = GetNearestFreeNode(root,
                        boxCutting.Width,
                        boxCutting.Height);

                    if (node != null)
                    {
                        // Build BoxCutting from Pattern
                        boxCutting = pattern.Build(new Sheet(node.Width, node.Height));
                    }
                }
                else
                {
                    node = FindNode(root, boxCutting);
                }


                if (node != null)
                {
                    var packResult = new BoxPackingResult
                    {
                        Cutting = boxCutting,
                        Position = new Point(node.X, node.Y)
                    };

                    packResults.Add(packResult);
                    node.Split(boxCutting);

                    // just continue use same BoxCutting, since it's most efficient
                    continue;
                }

                // move to another BoxCutting
                i++;
            }

            return packResults;
        }

        private Node FindNode(Node root, IBoxCutting boxCutting)
        {
            if (root.Used)
            {
                return FindNode(root.Right, boxCutting)
                       ?? FindNode(root.Up, boxCutting);
            }

            if (boxCutting.Width <= root.Width && boxCutting.Height <= root.Height)
            {
                return root;
            }

            return null;
        }

        private Node GetNearestFreeNode(Node root,
            int? minWidth = null,
            int? minHeight = null)
        {
            if (root.Used)
            {
                return GetNearestFreeNode(root.Right, minWidth, minHeight)
                       ?? GetNearestFreeNode(root.Up, minWidth, minHeight);
            }

            if ((!minHeight.HasValue || root.Height >= minHeight)
                && (!minWidth.HasValue || root.Width >= minWidth))
            {
                return root;
            }

            return null;
        }
    }

    public class Node
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public bool Used => Right != null && Up != null;

        public Node Right { get; private set; }
        public Node Up { get; private set; }

        public void Split(IBoxCutting boxCutting)
        {
            Right = new Node
            {
                X = X + boxCutting.Width,
                Y = Y,
                Width = Width - boxCutting.Width,
                Height = Height
            };

            Up = new Node
            {
                X = X,
                Y = Y + Height,
                Width = boxCutting.Width,
                Height = Height - boxCutting.Height
            };
        }
    }
}