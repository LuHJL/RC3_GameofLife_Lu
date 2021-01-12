using System.Collections;
using System.Collections.Generic;
using RC3.GameOfLifeStack;
using UnityEngine;
using XCharts ;



namespace RC3
{
    [DisallowMultipleComponent]
    [ExecuteInEditMode]
    public class XchartLineChart : MonoBehaviour
    {
        //for linechart

        private LineChart lineChart;
        private StackModelManager _modelManager;
        private CellLayer _cellLayer;

        private int currentLayer = 0;
        private int initCount = 0;

        void Awake()
        {
            lineChart = transform.Find("LineChart").gameObject.GetComponent<LineChart>();
            lineChart.ClearData();
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (initCount <= 90)
            {
                initCount++;
                AddOneData();
            }
            
            
        }

        public LineChart XchartLineChartineChat
        {
            get { return lineChart; }
        }


        public void AddOneData()
        {
            
            lineChart.title.text = "Mean Density in " + initCount + " layer";
            var yvalue = _cellLayer.Density;

            lineChart.AddData(0, yvalue);
            lineChart.AddXAxisData(initCount.ToString());

        }
    }
}
