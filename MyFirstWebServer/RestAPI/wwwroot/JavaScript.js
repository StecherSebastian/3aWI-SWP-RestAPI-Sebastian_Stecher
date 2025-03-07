"use strict";

async function getDataEndpoint1() {
    const input = document.getElementById("endpoint1_input").value
    Console.log(`Input ${input}`)
    const url = `http://localhost:5116/api/Put/${input}`;
    const request = { method: 'PUT' }
    try {
        const response = await fetch(url, request);
        if (!response.ok) {
            throw new Error(`Response status: ${response.status}`);
        }
        const output = await response.json();
        console.log(output);
        const outputLocation = "endpoint1_output";
        document.getElementById(outputLocation).innerText = output;
    } catch (error) {
        console.error(error.message);
    }
}

async function getDataEndpoint2() {
    const input1 = document.getElementById("endpoint2_input1").value;
    const input2 = document.getElementById("endpoint2_input2").value;
    const input = {
        input1: input1,
        input2: input2
    }
    const url = "http://localhost:5116/api/Put/SumOfInputs";
    const request = {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(input)
    }
    try {
        const response = await fetch(url, request);
        if (!response.ok) {
            throw new Error(`Response status: ${response.status}`);
        }
        const output = await response.json();
        console.log(output);
        const outputLocation = "endpoint2_output";
        document.getElementById(outputLocation).innerText = output;
    } catch (error) {
        console.error(error.message);
    }
}