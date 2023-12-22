async function getAllCars() {
    try {
        const response = await fetch('https://localhost:7172/api/Car/GetAllCars', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${localStorage.getItem('userToken')}`,
            },
        });

        if (response.ok) {
            const data = await response.json();

            const carContainer = document.getElementById('container');
            carContainer.innerHTML = '';

            data.forEach(car => {

                const containerCar = document.createElement('div');
                containerCar.classList.add('container-car');

                const carDiv = document.createElement('div');
                carDiv.classList.add('car');

                const carImage = document.createElement('img');
                carImage.classList.add('car-image-refactor');
                carImage.src = car.image;
                carImage.alt = car.name;

                const carName = document.createElement('p');
                carName.classList.add('car-name');
                carName.textContent = car.name;

                carDiv.appendChild(carImage);
                carDiv.appendChild(carName);

                const buttonDiv = document.createElement('div');
                buttonDiv.classList.add('column');

                const carInfoButton = document.createElement('button');
                carInfoButton.value = car.carUid;
                carInfoButton.id = 'carInfo';
                carInfoButton.classList.add('cars-page');
                carInfoButton.addEventListener('click', selectCar);
                carInfoButton.innerHTML = '<p>Страница машины</p>';

                const deleteCarButton = document.createElement('button');
                deleteCarButton.value = car.carUid;
                deleteCarButton.id = 'deleteCar';
                deleteCarButton.classList.add('delete-car-button');
                deleteCarButton.addEventListener('click', deleteCar);
                deleteCarButton.innerHTML = `<img class="car-image-delete_button" src="/icon/free-icon-delete-1214428.png" alt="${car.name}">`;

                buttonDiv.appendChild(carInfoButton);
                buttonDiv.appendChild(deleteCarButton);

                containerCar.appendChild(carDiv);
                containerCar.appendChild(buttonDiv);

                carContainer.appendChild(containerCar);
            });
        } else {
            console.log(await response.text())
            //location.reload(true);
            throw new Error('Что-то пошло не так');
        }
    } catch (error) {
        console.error(error);
    }
}

async function getCarInfo() {
    const uid = localStorage.getItem('carUid');

    try {
        const response = await fetch(`https://localhost:7172/api/Car/GetCarInfo?carUid=${uid}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${localStorage.getItem('userToken')}`,
            },
        });

        if (response.ok) {
            const data = await response.json();

            const carContainer = document.getElementById('container');
            carContainer.innerHTML = `
                <h1 class="car-title" id="carName">${data.name}</h1>
                <img class="car-image" src="${data.image}" alt="${data.name}" id="carImage">
                <div class="container-description">
                    <p class="car-description" id="carDescription">
                        ${data.description}
                    </p>
                </div>
                <a href="car_telemetry.html" class="telemetry-button" value=${uid}>Телеметрия</a>
            `
        } else {
            console.log(await response.text())
            throw new Error('Что-то пошло не так');
        }
    } catch (error) {
        console.error(error);
    }
}

async function getCarTelemetry() {
    const uid = localStorage.getItem('carUid');

    try {
        const response = await fetch(`https://localhost:7172/api/Car/GetCarTelemetry?carUid=${uid}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${localStorage.getItem('userToken')}`,
            },
        });

        if (response.ok) {
            const data = await response.json();

            const carContainer = document.getElementById('container');
            carContainer.innerHTML = `
                <h1 class="car-title">${data.name}</h1>
                <h2 class="section-title">Телеметрия</h2>
                <div class="info-container">
                    <h3 class="info-title">Скорость</h3>
                    <div class="info-container-description">
                        <p class="car-info-description">Максимальная <br>скорость, км/ч: ${data.maxSpeed}</p>
                        <p class="car-info-description">Средняя <br>скорость, км/ч: ${data.averageSpeed}</p>
                        <p class="car-info-description">Скорость на <br>поворотах, км/ч: ${data.corneringSpeed}</p>
                    </div>
                    <h3 class="info-title">Ускорение</h3>
                    <div class="info-container-description">
                        <p class="car-info-description">Максимальное <br>ускорение, м/сек^2: ${data.maxAcceleration}</p>
                        <p class="car-info-description">Среднее <br>ускорение, м/сек^2: ${data.averageAcceleration}</p>
                    </div>
                    <h3 class="info-title">Торможение</h3>
                    <div class="info-container-description">
                        <p class="car-info-description">Максимальное <br>торможение, м/сек^2: ${data.maxBraking}</p>
                        <p class="car-info-description">Среднее <br>торможение, м/сек^2: ${data.averageBraking}</p>
                    </div>
                    <h3 class="info-title">Двигатель</h3>
                    <div class="info-container-description">
                        <p class="car-info-description">Обороты <br>двигателя, обороты/мин: ${data.engineSpeed}</p>
                        <p class="car-info-description">Мощность <br>двигателя, лс: ${data.enginePower}</p>
                    </div>
                    <h3 class="info-title">Подвеска</h3> 
                    <div class="info-container-description">
                        <p class="car-info-description">Амплитуда <br>колебаний подвески, мм: ${data.suspensionVibrationAmplitude}</p>
                        <p class="car-info-description">Скорость <br>колебаний подвески, гц: ${data.suspensionVibrationSpeed}</p>
                    </div>
                </div>
            `;
        } else {
            console.log(await response.text())
            location.reload(true);
            throw new Error('Что-то пошло не так');
        }
    } catch (error) {
        console.error(error);
    }
}

/*async function createCar() {
    const carName = document.getElementById('carName').value;

    if (!carName) {
        alert('Введите название страны');
        return;
    }

    try {
        const response = await fetch(`https://localhost:7172/api/Car/CreateCountry?name=${carName}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${localStorage.getItem('userToken')}`
            },
        });

        const data = await response.text();

        if (response.ok) {
            console.log(data);;
            getAllCars();
        } else {
            console.log(data);
            throw new Error('Что-то пошло не так');
        }
    } catch (error) {
        console.error(error);
        alert('Ошибка');
    }
}*/

async function deleteCar() {
    const uid = getElementById('deleteCar').value;

    try {
        const response = await fetch(`https://localhost:7172/api/Car/DeleteCar?carUid=${uid}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${localStorage.getItem('userToken')}`
            },
        });

        const data = await response.text();

        if (response.ok) {
            console.log(data);
            getAllCars();
        } else {
            console.log(data);
            throw new Error('Что-то пошло не так');
        }
    } catch (error) {
        console.error(error);
        alert('Ошибка');
    }
 
}
