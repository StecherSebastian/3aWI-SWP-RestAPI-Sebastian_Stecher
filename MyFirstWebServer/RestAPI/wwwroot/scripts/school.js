"use strict";
document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("school").addEventListener("submit", async function (event) {
        event.preventDefault();
        await newSchool(this);
    });
    document.getElementById("classroom").addEventListener("submit", async function (event) {
        event.preventDefault();
        await newClassroom(this);
    });
    document.getElementById("student").addEventListener("submit", async function (event) {
        event.preventDefault();
        await newStudent(this);
    });
    populateDropdown(".school-dropdown", "http://localhost:5116/api/School/listSchools", "School");
    populateDropdown(".classroom-dropdown", "http://localhost:5116/api/School/listClassrooms", "Classroom");
    populateDropdown(".student-dropdown", "http://localhost:5116/api/School/listStudents", "Student");
    document.getElementById("addStudentToSchool").addEventListener("submit", async function (event) {
        event.preventDefault();
        await addStudentToSchool(this);
    });
    document.getElementById("addClassroomToSchool").addEventListener("submit", async function (event) {
        event.preventDefault();
        await addClassroomToSchool(this);
    });
    document.getElementById("addStudentToClassroom").addEventListener("submit", async function (event) {
        event.preventDefault();
        await addStudentToClassroom(this);
    });
    document.querySelectorAll(".school-dropdown").forEach(dropdown => {
        dropdown.addEventListener("change", async function (event) {
            await updateSchoolValues(Number(this.value));
        });
    });
})
async function newSchool() {
    const url = `http://localhost:5116/api/School/newSchool`;
    const request = { method: 'PUT' }
    try {
        const response = await fetch(url, request);
        if (!response.ok) {
            throw new Error(`Response status: ${response.status}`);
        }
        const output = await response.json();
        console.log(output);
        const outputLocation = "output";
        document.getElementById(outputLocation).innerText = JSON.stringify(output, null, 2);
        await populateDropdown(".school-dropdown", "http://localhost:5116/api/School/listSchools", "School");
    } catch (error) {
        console.error(error.message);
    }
}
async function newClassroom(form) {
    const input = new FormData(form)
    const classroom = {
        size: Number(input.get("size")),
        seats: Number(input.get("seats")),
        cynap: input.get("cynap") == "on"
    }
    console.log(`classroom: ${JSON.stringify(classroom)}`)
    const url = `http://localhost:5116/api/School/newClassroom`;
    const request = {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(classroom)
    }
    try {
        const response = await fetch(url, request);
        if (!response.ok) {
            throw new Error(`Response status: ${response.status}`);
        }
        const output = await response.json();
        const outputLocation = "output";
        document.getElementById(outputLocation).innerText = JSON.stringify(output, null, 2);
        await populateDropdown(".classroom-dropdown", "http://localhost:5116/api/School/listClassrooms", "Classroom");
    } catch (error) {
        console.error(error.message)
    }
}
async function newStudent(form) {
    const input = new FormData(form)
    const student = {
        schoolclass: Number(input.get("schoolclass")),
        gender: Number(input.get("gender")),
        birthdate: input.get("birthdate")
    }
    console.log(`student: ${JSON.stringify(student)}`)
    const url = `http://localhost:5116/api/School/newStudent`;
    const request = {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(student)
    }
    try {
        const response = await fetch(url, request);
        if (!response.ok) {
            throw new Error(`Response status: ${response.status}`);
        }
        const output = await response.json();
        const outputLocation = "output";
        document.getElementById(outputLocation).innerText = JSON.stringify(output, null, 2);
        await populateDropdown(".student-dropdown", "http://localhost:5116/api/School/listStudents", "Student");
    } catch (error) {
        console.error(error.message)
    }
}
async function populateDropdown(dropdownClass, url, dropdownType) {
    try {
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error(`Response status: ${response.status}`);
        }
        const data = await response.json();
        const dropdowns = document.querySelectorAll(dropdownClass);
        for (let dropdown of dropdowns) {
            dropdown.innerHTML = `<option value="default" disabled selected>Select an ${dropdownType}</option>`;
            Object.keys(data).forEach(key => {
                const option = document.createElement('option');
                option.value = key;
                option.textContent = `${dropdownType}-${key}`;
                dropdown.appendChild(option);
            });
        }
    } catch (error) {
        console.error(`Error fetching data for ${dropdownClass}:`, error.message);
    }
}
async function addStudentToSchool(form) {
    const input = new FormData(form);
    console.log(input.get("school"), input.get("student"));
    const schoolID = input.get("school");
    const studentID = input.get("student");
    const url = `http://localhost:5116/api/School/addStudentToSchool`;
    const request = {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ SchoolID: Number(schoolID), StudentID: Number(studentID) })
    }
    try {
        const response = await fetch(url, request);
        if (!response.ok) {
            throw new Error(`Response status: ${response.status}`);
        }
        const output = await response.json();
        const outputLocation = "output";
        document.getElementById(outputLocation).innerText = JSON.stringify(output, null, 2);
    } catch (error) {
        console.error(error.message)
    }
}
async function addClassroomToSchool(form) {
    const input = new FormData(form);
    console.log(input.get("school"), input.get("classroom"));
    const schoolID = input.get("school");
    const classroomID = input.get("classroom");
    const url = `http://localhost:5116/api/School/addClassroomToSchool`;
    const request = {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ SchoolID: Number(schoolID), ClassroomID: Number(classroomID) })
    }
    try {
        const response = await fetch(url, request);
        if (!response.ok) {
            throw new Error(`Response status: ${response.status}`);
        }
        const output = await response.json();
        const outputLocation = "output";
        document.getElementById(outputLocation).innerText = JSON.stringify(output, null, 2);
    } catch (error) {
        console.error(error.message)
    }
}
async function addStudentToClassroom(form) {
    const input = new FormData(form);
    console.log(input.get("classroom"), input.get("student"));
    const classroomID = input.get("classroom");
    const studentID = input.get("student");
    const url = `http://localhost:5116/api/School/addStudentToClassroom`;
    const request = {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ ClassroomID: Number(classroomID), StudentID: Number(studentID) })
    }
    try {
        const response = await fetch(url, request);
        if (!response.ok) {
            throw new Error(`Response status: ${response.status}`);
        }
        const output = await response.json();
        const outputLocation = "output";
        document.getElementById(outputLocation).innerText = JSON.stringify(output, null, 2);
    } catch (error) {
        console.error(error.message);
    }
}
async function updateSchoolValues(schoolID) {
    try {
        const numberOfStudents = await fetchValue(`http://localhost:5116/api/School/NumberOfStudents`, { SchoolID: schoolID });
        document.getElementById("numberOfStudents").innerText = numberOfStudents;

        const numberOfMaleStudents = await fetchValue(`http://localhost:5116/api/School/NumberOfMaleStudents`, { SchoolID: schoolID });
        document.getElementById("numberOfMaleStudents").innerText = numberOfMaleStudents;

        const numberOfFemaleStudents = await fetchValue(`http://localhost:5116/api/School/NumberOfFemaleStudents`, { SchoolID: schoolID });
        document.getElementById("numberOfFemaleStudents").innerText = numberOfFemaleStudents;

        const averageAgeOfStudents = await fetchValue(`http://localhost:5116/api/School/AverageAgeOfStudents`, { SchoolID: schoolID });
        document.getElementById("averageAgeOfStudents").innerText = averageAgeOfStudents;

        const numberOfClassrooms = await fetchValue(`http://localhost:5116/api/School/NumberOfClassrooms`, { SchoolID: schoolID });
        document.getElementById("numberOfClassrooms").innerText = numberOfClassrooms;

        const classroomsWithCynap = await fetchValue(`http://localhost:5116/api/School/ClassroomsWithCynap`, { SchoolID: schoolID });
        document.getElementById("classroomsWithCynap").innerText = classroomsWithCynap;

        const classroomsWithNumberOfStudents = await fetchValue(`http://localhost:5116/api/School/ClassroomsWithNumberOfStudents`, { SchoolID: schoolID });
        document.getElementById("classroomsWithNumberOfStudents").innerText = classroomsWithNumberOfStudents;
    } catch (error) {
        console.error(error.message);
    }
}
async function fetchValue(url, body) {
    const request = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(body)
    }
    const response = await fetch(url, request);
    if (!response.ok) {
        throw new Error(`Response status: ${response.status}`);
    }
    const data = await response.json();
    return JSON.stringify(data, null, 2);
}