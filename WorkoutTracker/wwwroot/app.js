const workoutsList = document.getElementById('workoutsList');
const workoutSelect = document.getElementById('workoutSelect');
const errorBox = document.getElementById('errorBox');
const muscleFilter = document.getElementById('muscleFilter');

document.getElementById('workoutDate').valueAsDate = new Date();

function showError(msg) {
    errorBox.textContent = msg;
    errorBox.classList.remove('hidden');
    setTimeout(() => errorBox.classList.add('hidden'), 5000);
}

async function handleApiError(response) {
    const data = await response.json().catch(() => null);
    if (data && data.errors) {
        const messages = Object.values(data.errors).flat().join(' ');
        showError(messages);
    } else {
        showError(typeof data === 'string' ? data : 'Request failed.');
    }
}

async function loadWorkouts() {
    const res = await fetch('/api/Workouts');
    const workouts = await res.json();
    const filter = muscleFilter.value.trim().toLowerCase();

    workoutSelect.innerHTML = '<option value="">Select Workout...</option>';
    workouts.forEach(w => {
        const opt = document.createElement('option');
        opt.value = w.id;
        opt.textContent = `${w.title} (${new Date(w.date).toLocaleDateString()})`;
        workoutSelect.appendChild(opt);
    });

    workoutsList.innerHTML = workouts.map(w => {
        const filteredExercises = filter
            ? w.exercises.filter(e => e.muscleGroup.toLowerCase().includes(filter))
            : w.exercises;

        return `
            <div class="workout-card">
                <div class="workout-top">
                    <div>
                        <h3>${w.title} <small style="color:#94a3b8">(${new Date(w.date).toLocaleDateString()})</small></h3>
                        ${w.notes ? `<p class="notes">${w.notes}</p>` : ''}
                    </div>
                    <div style="display:flex; gap:8px; align-items:center;">
                        <span class="volume-badge">Total: ${w.totalVolumeKg} kg</span>
                        <button class="btn-delete" onclick="deleteWorkout(${w.id})">Delete</button>
                    </div>
                </div>
                ${filteredExercises.length ? `
                    <table>
                        <thead>
                            <tr>
                                <th>Exercise</th>
                                <th>Muscle</th>
                                <th>Weight</th>
                                <th>Reps</th>
                                <th>Volume</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            ${filteredExercises.map(e => `
                                <tr>
                                    <td>${e.name}</td>
                                    <td>${e.muscleGroup}</td>
                                    <td>${e.weightKg} kg</td>
                                    <td>${e.reps}</td>
                                    <td>${e.volumeKg} kg</td>
                                    <td><button class="btn-delete" onclick="deleteExercise(${e.id})">X</button></td>
                                </tr>
                            `).join('')}
                        </tbody>
                    </table>
                ` : '<p class="notes">No exercises yet.</p>'}
            </div>
        `;
    }).join('');
}

document.getElementById('workoutForm').addEventListener('submit', async (e) => {
    e.preventDefault();
    const payload = {
        title: document.getElementById('workoutTitle').value,
        date: document.getElementById('workoutDate').value,
        notes: document.getElementById('workoutNotes').value || null
    };

    const res = await fetch('/api/Workouts', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload)
    });

    if (!res.ok) return handleApiError(res);
    e.target.reset();
    document.getElementById('workoutDate').valueAsDate = new Date();
    loadWorkouts();
});

document.getElementById('exerciseForm').addEventListener('submit', async (e) => {
    e.preventDefault();
    const payload = {
        workoutId: Number(workoutSelect.value),
        name: document.getElementById('exerciseName').value,
        muscleGroup: document.getElementById('muscleGroup').value,
        weightKg: Number(document.getElementById('weightKg').value),
        reps: Number(document.getElementById('reps').value)
    };

    const res = await fetch('/api/Exercises', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload)
    });

    if (!res.ok) return handleApiError(res);
    document.getElementById('exerciseName').value = '';
    document.getElementById('weightKg').value = '';
    document.getElementById('reps').value = '';
    loadWorkouts();
});

async function deleteWorkout(id) {
    await fetch(`/api/Workouts/${id}`, { method: 'DELETE' });
    loadWorkouts();
}

async function deleteExercise(id) {
    await fetch(`/api/Exercises/${id}`, { method: 'DELETE' });
    loadWorkouts();
}

muscleFilter.addEventListener('input', loadWorkouts);
loadWorkouts();