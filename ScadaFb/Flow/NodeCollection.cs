using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace MasterScadaBlocks.Flow
{
    public class NodeCollection
    {
        #region Private Fields

        private const double GAP = 0.05;
        private const double HEIGHT = 400;
        private const int MAXLEVELS = 10;
        private const int MAXNODES = 10;
        private const double WIDTH = 800;
        private readonly Dictionary<int, Node> nodes;
        private List<NodeLink> nodesLink;

        #endregion Private Fields

        #region Public Constructors

        public NodeCollection()
        {
            nodes = new Dictionary<int, Node>();
            nodesLink = new List<NodeLink>();

            Levels = 2;
        }

        #endregion Public Constructors



        #region Public Properties

        public int Levels { get; private set; }

        #endregion Public Properties



        #region Public Methods

        public void AddLevels(int levelIndex, string orders)
        {
            if (orders.Trim() == "0" || orders.Trim() == "")
            {
                return;
            }

            string[] order = orders.Trim().Split(' ');

            try
            {
                int i = 0;
                foreach (string o in order)
                {
                    int index = int.Parse(o) - 1;
                    nodes[index].Level = levelIndex;
                    nodes[index].OrderInLevel = i;
                    i++;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("AddLevels\n" + ex.Message);
            }

            Levels = Math.Max(Levels, levelIndex + 1);
        }

        public void AddNode(int index, string name)
        {
            AddNode(index, name, Color.FromArgb(200, 0, 0, 0));
        }

        public void AddNode(int index, string name, Color color)
        {
            nodes.Add(index, new Node(name, color));
        }

        public void AddNodeLink(int inputIndex, int outputIndex, double value)
        {
            if (nodes.ContainsKey(inputIndex) && nodes.ContainsKey(outputIndex))
            {
                nodesLink.Add(new NodeLink(nodes[inputIndex], nodes[outputIndex], value));
            }
        }

        public void AddNodeLink(string str, double value)
        {
            try
            {
                string[] arr = str.Trim().Split(' ');
                if (arr.Length != 2 || arr[0] == "0" || arr[1] == "0")
                {
                    return;
                }

                int.TryParse(arr[0], out int begin);
                int.TryParse(arr[1], out int end);
                if (begin > 0 && end > 0)
                {
                    AddNodeLink(begin - 1, end - 1, value);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        public void Paint(Canvas canvas)
        {
            canvas.Children.Clear();

            // устанавливаем величины Value в узлах
            SetNodeValues();

            // определеям макс. величину на уровне
            double baseValue = 1;
            double gap = 0;
            for (int i = 0; i < Levels; i++)
            {
                IEnumerable<double> nodesInLevel = from node in nodes.Values
                                                   where node.Level == i && node.Value > 0
                                                   select node.Value;
                double tempBaseValue = nodesInLevel.Sum() * (1 + GAP * (nodesInLevel.Count() - 1));

                if (tempBaseValue > baseValue)
                {
                    baseValue = tempBaseValue;
                    if ((nodesInLevel.Count() - 1) > 0)
                    {
                        gap = (baseValue - nodesInLevel.Sum()) / (nodesInLevel.Count() - 1);
                    }
                }
            }

            // определяем координаты прямоугольников по уровням
            for (int i = 0; i < Levels; i++)
            {
                IEnumerable<Node> nodesInLevel = from node in nodes.Values
                                                 where node.Level == i && node.Value > 0
                                                 orderby node.OrderInLevel
                                                 select node;

                double sum = (baseValue - (nodesInLevel.Sum(n => n.Value) + gap * (nodesInLevel.Count() - 1))) / 2;

                foreach (Node node in nodesInLevel)
                {
                    node.SetPosition(0.85 * WIDTH / (Levels - 1), HEIGHT / baseValue, sum);
                    sum += node.Value + gap;
                }
            }

            // определяем предварительные координаты связей
            foreach (NodeLink nodeLink in nodesLink)
            {
                nodeLink.SetPosition(HEIGHT / baseValue);
            }

            nodesLink.Sort();

            foreach (Node node in nodes.Values)
            {
                node.InputUsed = 0;
                node.OutputUsed = 0;
            }

            // определяем точные координаты и рисуем связи
            foreach (NodeLink link in nodesLink)
            {
                link.SetPosition(HEIGHT / baseValue);
                link.Paint(canvas);
            }

            // рисуем узлы
            foreach (Node node in nodes.Values)
            {
                node.Paint(canvas);
            }
        }

        public override string ToString()
        {
            string str = "Узлы\n";

            foreach (Node node in nodes.Values)
            {
                str += node.ToString() + "\n";
            }

            str += "\nСвязи\n";

            foreach (NodeLink nodeLink in nodesLink)
            {
                str += nodeLink.ToString() + "\n";
            }

            return str;
        }

        #endregion Public Methods



        #region Private Methods

        private void SetNodeValues()
        {
            foreach (Node node in nodes.Values)
            {
                double sumInput = (from n in nodesLink
                                   where n.Input == node
                                   select n.Value).Sum();
                double sumOutput = (from n in nodesLink
                                    where n.Output == node
                                    select n.Value).Sum();

                node.Value = Math.Max(sumInput, sumOutput);
            }

            // определяем процент от общего значения на уровне
            for (int i = 0; i < Levels; i++)
            {
                IEnumerable<Node> nodesInLevel = from node in nodes.Values
                                                 where node.Level == i
                                                 select node;
                double sum = nodesInLevel.Sum(n => n.Value);
                foreach (Node node in nodesInLevel)
                {
                    node.ValuePercent = 100 * node.Value / sum;
                }
            }

            #endregion Private Methods
        }
    }
}