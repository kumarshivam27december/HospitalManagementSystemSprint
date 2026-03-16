//console.log("Hospital System Loaded");
//// Day 7: JavaScript ES6 Implementation

//// Mock Data for Simulation (Before API Integration)
//let doctors = [
//    { id: 1, name: "Dr. Sharma", specialization: "Cardiology", fee: 800.00 },
//    { id: 2, name: "Dr. Verma", specialization: "Neurology", fee: 1200.00 }
//];

//let patients = [
//    { id: 101, name: "Rahul Kumar", age: 30, condition: "Heart Checkup", date: "2026-03-15", doctorId: 1 }
//];

//// 1. Wait for DOM to load
//document.addEventListener('DOMContentLoaded', () => {
//    console.log("Hospital System Loaded...");

//    // Determine which page we are on and initialize
//    if (document.getElementById('doctorTableBody')) renderDoctors();
//    if (document.getElementById('patientTableBody')) renderPatients();

//    // 2. Add Event Listeners for Forms (Task 9)
//    const doctorForm = document.getElementById('doctorForm');
//    if (doctorForm) {
//        doctorForm.addEventListener('submit', (e) => handleFormSubmit(e, 'doctor'));
//    }

//    const patientForm = document.getElementById('patientForm');
//    if (patientForm) {
//        patientForm.addEventListener('submit', (e) => handleFormSubmit(e, 'patient'));
//    }
//});

//// 3. Arrow Function for Form Handling (Task 8)
//const handleFormSubmit = (event, type) => {
//    event.preventDefault(); // Task 9: preventDefault()

//    if (type === 'doctor') {
//        const newDoctor = {
//            id: doctors.length + 1,
//            name: document.getElementById('doctorName').value,
//            specialization: document.getElementById('specialization').value,
//            fee: parseFloat(document.getElementById('fee').value)
//        };

//        // Basic Client-Side Validation (Task 11)
//        if (newDoctor.fee <= 0) {
//            alert("Fee must be a positive number!");
//            return;
//        }

//        doctors.push(newDoctor);
//        alert("Doctor added successfully!");
//        window.location.href = "list-doctors.html";
//    }

//    if (type === 'patient') {
//        const newPatient = {
//            id: 100 + patients.length + 1,
//            name: document.getElementById('patientName').value,
//            age: parseInt(document.getElementById('age').value),
//            condition: document.getElementById('condition').value,
//            date: document.getElementById('appointmentDate').value,
//            doctorId: parseInt(document.getElementById('doctorId').value)
//        };

//        // Validate Doctor Exists (Logic check)
//        const doctorExists = doctors.some(d => d.id === newPatient.doctorId);
//        if (!doctorExists) {
//            alert("Error: Doctor ID does not exist!");
//            return;
//        }

//        patients.push(newPatient);
//        alert("Patient registered successfully!");
//        window.location.href = "list-patients.html";
//    }
//};

//// 4. Dynamic Table Rendering (Task 10)
//const renderDoctors = () => {
//    const tbody = document.getElementById('doctorTableBody');
//    if (!tbody) return;

//    tbody.innerHTML = ""; // Clear existing
//    doctors.forEach((doc) => {
//        const row = document.createElement('tr');
//        row.innerHTML = `
//            <td data-label="ID">${doc.id}</td>
//            <td data-label="Name">${doc.name}</td>
//            <td data-label="Spec">${doc.specialization}</td>
//            <td data-label="Fee">₹${doc.fee.toFixed(2)}</td>
//            <td data-label="Actions">
//                <button class="btn-edit" onclick="alert('Edit logic coming in Day 8!')">Edit</button>
//                <button class="btn-delete" onclick="deleteItem(${doc.id}, 'doctor')">Delete</button>
//            </td>
//        `;
//        tbody.appendChild(row);
//    });
//};

//const renderPatients = () => {
//    const tbody = document.getElementById('patientTableBody');
//    if (!tbody) return;

//    tbody.innerHTML = "";
//    patients.forEach((p) => {
//        const row = document.createElement('tr');
//        row.innerHTML = `
//            <td data-label="ID">${p.id}</td>
//            <td data-label="Name">${p.name}</td>
//            <td data-label="Age">${p.age}</td>
//            <td data-label="Cond">${p.condition}</td>
//            <td data-label="Date">${p.date}</td>
//            <td data-label="Doc ID">${p.doctorId}</td>
//            <td data-label="Actions">
//                <button class="btn-delete" onclick="deleteItem(${p.id}, 'patient')">Delete</button>
//            </td>
//        `;
//        tbody.appendChild(row);
//    });
//};

//// 5. Delete Row Function (Task 12)
//const deleteItem = (id, type) => {
//    if (!confirm("Are you sure you want to delete this record?")) return;

//    if (type === 'doctor') {
//        doctors = doctors.filter(d => d.id !== id);
//        renderDoctors();
//    } else {
//        patients = patients.filter(p => p.id !== id);
//        renderPatients();
//    }
//};

/**
 * DAY 7: ADVANCED FRONTEND LOGIC
 * Focus: ES6 Syntax, Event Listeners, and Dynamic Rendering
 */

// TASK 7: ES6 Basics (let & const)
const hospitalName = "Smart Hospital Management"; // Immutable
let doctors = [
    { id: 1, name: "Dr. Sharma", fee: 800, specialization: "Cardiology" },
    { id: 2, name: "Dr. Smith", fee: 1200, specialization: "Neurology" }
]; // Mutable array to store local data

// Wait for DOM to fully load before attaching events
document.addEventListener('DOMContentLoaded', () => {

    // TASK 9: addEventListener() for Form Submission
    const doctorForm = document.getElementById('doctorForm');
    if (doctorForm) {
        doctorForm.addEventListener('submit', (event) => {
            // preventDefault() stops the page from refreshing
            event.preventDefault();
            addDoctor();
        });
    }

    // Initialize table if we are on the list page
    if (document.getElementById('doctorTableBody')) {
        renderTable();
    }
});

// TASK 8: Arrow Functions & Template Literals
const addDoctor = () => {
    // Collect values from the DOM
    const name = document.getElementById('doctorName').value;
    const spec = document.getElementById('specialization').value;
    const fee = document.getElementById('fee').value;

    // TASK 11: Client-Side Validation
    if (!name || fee <= 0) {
        alert("Please enter a valid name and fee (> 0).");
        return;
    }

    // Create a new doctor object
    const newDoc = {
        id: doctors.length + 1,
        name: name,
        specialization: spec,
        fee: parseFloat(fee)
    };

    // Add to the local array
    doctors.push(newDoc);

    // Notify user and refresh the UI
    alert(`Success: ${newDoc.name} registered.`);
    renderTable();

    // Reset the form
    document.getElementById('doctorForm').reset();
};

// TASK 10: Dynamic Table Rendering
const renderTable = () => {
    const tbody = document.getElementById('doctorTableBody');
    if (!tbody) return;

    // Clear existing rows
    tbody.innerHTML = "";

    // Use forEach to iterate and Template Literals to build rows
    doctors.forEach((doc, index) => {
        const row = `
            <tr>
                <td>${doc.id}</td>
                <td>${doc.name}</td>
                <td>${doc.specialization}</td>
                <td>₹${doc.fee.toFixed(2)}</td>
                <td>
                    <button class="btn-delete" onclick="deleteDoctor(${index})">Delete</button>
                </td>
            </tr>
        `;
        tbody.innerHTML += row; // Update DOM
    });
};

// TASK 12: Delete Row Functionality
const deleteDoctor = (index) => {
    // Confirmation dialog before deletion
    if (confirm("Are you sure you want to remove this doctor?")) {
        // Use splice to remove the specific item from the array
        doctors.splice(index, 1);
        renderTable(); // Re-render to show updated list
    }
};

// TASK 11: Live Search Feature
const filterDoctors = (keyword) => {
    // Use the filter method (ES6) to find matching names
    const filtered = doctors.filter(doc =>
        doc.name.toLowerCase().includes(keyword.toLowerCase())
    );

    // Update only the filtered rows in the table
    const tbody = document.getElementById('doctorTableBody');
    tbody.innerHTML = filtered.map(doc => `
        <tr>
            <td>${doc.id}</td>
            <td>${doc.name}</td>
            <td>${doc.specialization}</td>
            <td>₹${doc.fee.toFixed(2)}</td>
            <td><button onclick="alert('Filtered delete requires ID mapping')">Delete</button></td>
        </tr>
    `).join('');
};