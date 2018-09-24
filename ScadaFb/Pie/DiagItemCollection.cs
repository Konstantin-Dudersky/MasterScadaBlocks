using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MasterScadaBlocks.Pie
{
    public class DiagItemCollection : IEnumerable<DiagItem>
    {
        private List<DiagItem> items;
        public int Count => items.Count;

        public DiagItemCollection()
        {
            items = new List<DiagItem>();
        }

        public void Sort()
        {
            items.Sort();
        }

        public DiagItem this[int index] {
            get => items[index];
            set => items.Insert(index, value);
        }

        public void Add(string name, Color color)
        {
            items.Add(new DiagItem(name, color));
        }

        public void SetPercent()
        {
            double total = (from p in items select p.Value).Sum();

            foreach (DiagItem item in items)
            {
                item.SetPercent(total);
            }
        }

        public void Draw(Canvas canvas)
        {
            DrawHeader(canvas);
            DrawSegments(canvas);
            DrawLegend(canvas);
        }

        private void DrawHeader(Canvas canvas)
        {
            canvas.Children.Add(new Label
            {
                Content = "Заголовок",
                Height = 20,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 0),
                Padding = new Thickness(0),
                VerticalContentAlignment = VerticalAlignment.Center,
                Width = 400,
            });
        }

        private void DrawSegments(Canvas canvas)
        {
            double sum = 0;

            foreach (DiagItem item in items)
            {
                canvas.Children.Add(item.Segment(120, 120, 100, 3.6 * sum));
                sum += item.Percent;
            }
        }

        private void DrawLegend(Canvas canvas)
        {
            int index = 0;
            foreach (DiagItem item in (from i in items where i.Percent > 0 select i))
            {
                double top = index * DiagItem.LEGENDHEIGHT * 1.2 + 40;
                canvas.Children.Add(item.LegendRect(250, top));
                canvas.Children.Add(item.LegendName(270, top));
                canvas.Children.Add(item.LegendPercent(350, top));
                index++;
            }
        }

        public IEnumerator<DiagItem> GetEnumerator()
        {
            return new DiagItemEnum(items);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class DiagItemEnum : IEnumerator<DiagItem>
        {
            private List<DiagItem> items;
            private int position = -1;

            public DiagItemEnum(List<DiagItem> items)
            {
                this.items = items;
            }

            public DiagItem Current {
                get {
                    try
                    {
                        return items[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                position++;
                return (position < items.Count);
            }

            public void Reset()
            {
                position = -1;
            }
        }
    }
}