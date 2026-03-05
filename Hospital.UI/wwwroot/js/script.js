console.log("Hospital System Loaded");
// Day 7: JavaScript ES6 Implementation

// Mock Data for Simulation (Before API Integration)
let doctors = [
    { id: 1, name: "Dr. Sharma", specialization: "Cardiology", fee: 800.00 },
    { id: 2, name: "Dr. Verma", specialization: "Neurology", fee: 1200.00 }
];

let patients = [
    { id: 101, name: "Rahul Kumar", age: 30, condition: "Heart Checkup", date: "2026-03-15", doctorId: 1 }
];

// 1. Wait for DOM to load
document.addEventListener('DOMContentLoaded', () => {
    console.log("Hospital System Loaded...");

    // Determine which page we are on and initialize
    if (document.getElementById('doctorTableBody')) renderDoctors();
    if (document.getElementById('patientTableBody')) renderPatients();

    // 2. Add Event Listeners for Forms (Task 9)
    const doctorForm = document.getElementById('doctorForm');
    if (doctorForm) {
        doctorForm.addEventListener('submit', (e) => handleFormSubmit(e, 'doctor'));
    }

    const patientForm = document.getElementById('patientForm');
    if (patientForm) {
        patientForm.addEventListener('submit', (e) => handleFormSubmit(e, 'patient'));
    }
});

// 3. Arrow Function for Form Handling (Task 8)
const handleFormSubmit = (event, type) => {
    event.preventDefault(); // Task 9: preventDefault()

    if (type === 'doctor') {
        const newDoctor = {
            id: doctors.length + 1,
            name: document.getElementById('doctorName').value,
            specialization: document.getElementById('specialization').value,
            fee: parseFloat(document.getElementById('fee').value)
        };

        // Basic Client-Side Validation (Task 11)
        if (newDoctor.fee <= 0) {
            alert("Fee must be a positive number!");
            return;
        }

        doctors.push(newDoctor);
        alert("Doctor added successfully!");
        window.location.href = "list-doctors.html";
    }

    if (type === 'patient') {
        const newPatient = {
            id: 100 + patients.length + 1,
            name: document.getElementById('patientName').value,
            age: parseInt(document.getElementById('age').value),
            condition: document.getElementById('condition').value,
            date: document.getElementById('appointmentDate').value,
            doctorId: parseInt(document.getElementById('doctorId').value)
        };

        // Validate Doctor Exists (Logic check)
        const doctorExists = doctors.some(d => d.id === newPatient.doctorId);
        if (!doctorExists) {
            alert("Error: Doctor ID does not exist!");
            return;
        }

        patients.push(newPatient);
        alert("Patient registered successfully!");
        window.location.href = "list-patients.html";
    }
};

// 4. Dynamic Table Rendering (Task 10)
const renderDoctors = () => {
    const tbody = document.getElementById('doctorTableBody');
    if (!tbody) return;

    tbody.innerHTML = ""; // Clear existing
    doctors.forEach((doc) => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td data-label="ID">${doc.id}</td>
            <td data-label="Name">${doc.name}</td>
            <td data-label="Spec">${doc.specialization}</td>
            <td data-label="Fee">₹${doc.fee.toFixed(2)}</td>
            <td data-label="Actions">
                <button class="btn-edit" onclick="alert('Edit logic coming in Day 8!')">Edit</button>
                <button class="btn-delete" onclick="deleteItem(${doc.id}, 'doctor')">Delete</button>
            </td>
        `;
        tbody.appendChild(row);
    });
};

const renderPatients = () => {
    const tbody = document.getElementById('patientTableBody');
    if (!tbody) return;

    tbody.innerHTML = "";
    patients.forEach((p) => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td data-label="ID">${p.id}</td>
            <td data-label="Name">${p.name}</td>
            <td data-label="Age">${p.age}</td>
            <td data-label="Cond">${p.condition}</td>
            <td data-label="Date">${p.date}</td>
            <td data-label="Doc ID">${p.doctorId}</td>
            <td data-label="Actions">
                <button class="btn-delete" onclick="deleteItem(${p.id}, 'patient')">Delete</button>
            </td>
        `;
        tbody.appendChild(row);
    });
};

// 5. Delete Row Function (Task 12)
const deleteItem = (id, type) => {
    if (!confirm("Are you sure you want to delete this record?")) return;

    if (type === 'doctor') {
        doctors = doctors.filter(d => d.id !== id);
        renderDoctors();
    } else {
        patients = patients.filter(p => p.id !== id);
        renderPatients();
    }
};