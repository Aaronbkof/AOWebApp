import { useState, useEffect } from "react";
import * as d3 from "d3";

function LogToNum(input) {
    if (!input || typeof input !== 'string') { return 0 };
    var stringArray = input.split(/(\s+)/);

    for (const item of stringArray) {
        if (item.startsWith('gain:')) {
            let val = item.substring(5)
            return Number(val)
        }
    }
    return 0;
}

export default function Graph() {
    const [rngNumber, setRngNumber] = useState(0);
    const [rngArray, setRngArray] = useState([]);
    const maxItems = 50;
    const timeOut = 100;
    const maxValue = 1;

    useEffect(() => {
        const interval = setInterval(() => {
            let val = Math.random()
            setRngNumber(`3/8 → 7/16: note:d4 s:supersaw cutoff:300 attack:0 decay:0 sustain:0.5 release:0.1 room:0.6 lpenv:3.3 gain:${val} duration:0.10714285714285714 background-color: black;color:white;border-radius:15px`);
        }, timeOut);
        return () => clearInterval(interval);
    }, []);

    useEffect(() => {
        let tempArray = [...rngArray, rngNumber];
        if (tempArray.length > maxItems) { tempArray.shift() }
        setRngArray(tempArray);
    }, [rngNumber]);

    useEffect(() => {
        //select svg element
        const svg = d3.select('svg')
        svg.selectAll("*").remove();

        //set the Width and Height
        let w = svg.node().getBoundingClientRect().width
        w = w - 40
        let h = svg.node().getBoundingClientRect().height
        h = h - 25
        const barMargin = 10;
        const barWidth = w / rngArray.length

        //create y scale
        let yScale = d3.scaleLinear()
            .domain([0, maxValue])
            .range([h, 0]);

        //translate the bars for axis room
        const chartGroup = svg.append('g')
            .classed('chartGroup', true)
            .attr('transform', 'translate(30,3)');

        let barGroups = chartGroup.selectAll('g')
            .data(rngArray);

        //add bar groups
        let newBarGroups = barGroups.enter()
            .append('g')
            .attr('transform', (d, i) => {
                return `translate(${i * barWidth}, ${yScale(d)})`
            });

        //add yaxis to chartGroup
        let yAxis = d3.axisLeft(yScale);
        chartGroup.append('g')
            .classed('axis y', true)
            .call(yAxis);

        ////draw rectangles (colour)
        //newBarGroups
        //    .append('rect')
        //    .attr('height', 0)
        //    .attr('y', d => h - yScale(d))
        //    .attr('width', barWidth - barMargin)
        //    .attr('y', 0)
        //    .attr('height', d => h - yScale(d))
        //    .attr('fill', (d, i) => `rgb(${360 / maxValue * d + 1}, ${360 - (360 / maxValue * d + 1)}, 60)`)

        //gradient for line graph tracing
        chartGroup.append("linearGradient")
            .attr("id", "line-gradient")
            .attr("gradientUnits", "userSpaceOnUse")
            .attr("x1", 0)
            .attr("y1", yScale(0))
            .attr("x2", 0)
            .attr("y2", yScale(maxValue))
            .selectAll("stop")
            .data([
                { offset: "0%", color: "green" },
                { offset: "100%", color: "red" }
            ])
            .enter().append("stop")
            .attr("offset", function (d) { return d.offset; })
            .attr("stop-color", function (d) { return d.color; });

        //line drawing
        chartGroup
            .append('path')
            .datum(rngArray.map((d) => LogToNum(d)))
            .attr('fill', 'none')
            .attr('stroke', 'url(#line-gradient)')
            .attr('stroke-width', 1.5)
            .attr('d', d3.line()
                .x((d, i) => i * barWidth)
                .y(d => yScale(d))
            )

        //newBarGroups
        //    .append('rect')
        //    .attr('x', 0)
        //    .attr('height', d => { return h - yScale(d) })
        //    .attr('width', barWidth - barMargin)
        //    .attr('fill', 'black')

    }, [rngArray]);

    return (
        <div className="App container">
            <h1>
                RNG Output:{rngNumber}
            </h1>
            <div className="row">
                <svg width="100%" height="600px"
                    className="border border-primary rounded p-2"></svg>
            </div>
        </div>
    );
}