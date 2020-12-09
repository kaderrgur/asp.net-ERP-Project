var Script = function () {

    //morris chart

    $(function () {
      // data stolen from http://howmanyleft.co.uk/vehicle/jaguar_'e'_type
      var tax_data = [
           {"period": "2011 Q3", "licensed": 3407, "sorned": 660},
           {"period": "2011 Q2", "licensed": 3351, "sorned": 629},
           {"period": "2011 Q1", "licensed": 3269, "sorned": 618},
           {"period": "2010 Q4", "licensed": 3246, "sorned": 661},
           {"period": "2009 Q4", "licensed": 3171, "sorned": 676},
           {"period": "2008 Q4", "licensed": 3155, "sorned": 681},
           {"period": "2007 Q4", "licensed": 3226, "sorned": 620},
           {"period": "2006 Q4", "licensed": 3245, "sorned": null},
           {"period": "2005 Q4", "licensed": 3289, "sorned": null}
      ];
      Morris.Line({
        element: 'hero-graph',
        data: tax_data,
        xkey: 'period',
        ykeys: ['licensed', 'sorned'],
        labels: ['Is Ortaklari', 'Tasoron'],
        lineColors:['#8075c4','#6883a3']
      });

      Morris.Donut({
          element: 'hero-donut',
        data: [
          {label: 'Kumas Boyasi', value: 25 },
          {label: 'Kumas', value: 40 },
          {label: 'Iplik', value: 25 },
          {label: 'Dugme', value: 10 }
        ],
          colors: ['#41cac0', '#49e2d7', '#34a39b'],
        formatter: function (y) { return y + "%" }
      });

      Morris.Area({
        element: 'hero-area',
        data: [
          {period: '2010 Q1', fatura: 2666, ipad: null, itouch: 2647},
          {period: '2010 Q2', fatura: 2778, ipad: 2294, itouch: 2441},
          {period: '2010 Q3', fatura: 4912, ipad: 1969, itouch: 2501},
          {period: '2010 Q4', fatura: 3767, ipad: 3597, itouch: 5689},
          {period: '2011 Q1', fatura: 6810, ipad: 1914, itouch: 2293},
          {period: '2011 Q2', fatura: 5670, ipad: 4293, itouch: 1881},
          {period: '2011 Q3', fatura: 4820, ipad: 3795, itouch: 1588},
          {period: '2011 Q4', fatura: 15073, ipad: 5967, itouch: 5175},
          {period: '2012 Q1', fatura: 10687, ipad: 4460, itouch: 2028},
          {period: '2012 Q2', fatura: 8432, ipad: 5713, itouch: 1791}
        ],

          xkey: 'period',
          ykeys: ['fatura', 'ipad', 'itouch'],
          labels: ['Fatura', 'Satis', 'Kar'],
          hideHover: 'auto',
          lineWidth: 1,
          pointSize: 5,
          lineColors: ['#4a8bc2', '#ff6c60', '#a9d86e'],
          fillOpacity: 0.5,
          smooth: true
      });

      Morris.Bar({
        element: 'hero-bar',
        data: [
          {device: '2014', geekbench: 136},
          {device: '2015', geekbench: 137},
          {device: '2016', geekbench: 275},
          {device: '2017', geekbench: 380},
          {device: '2018', geekbench: 655},
          {device: '2019', geekbench: 1571}
        ],
        xkey: 'device',
        ykeys: ['geekbench'],
        labels: ['Geekbench'],
        barRatio: 0.4,
        xLabelAngle: 35,
        hideHover: 'auto',
        barColors: ['#6883a3']
      });

      new Morris.Line({
        element: 'examplefirst',
        xkey: 'year',
        ykeys: ['value'],
        labels: ['Value'],
        data: [
          {year: '2008', value: 20},
          {year: '2009', value: 10},
          {year: '2010', value: 5},
          {year: '2011', value: 5},
          {year: '2012', value: 20}
        ]
      });

      $('.code-example').each(function (index, el) {
        eval($(el).text());
      });
    });

}();




