async function login() {
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;

    const credentials = {
        email: email,
        password: password
    };

    try {
        const response = await fetch('https://localhost:7172/api/User/Login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(credentials)
        });

        if (response.ok) {
            const data = await response.json();
            localStorage.setItem('userToken', data.token);

            var decodedToken = JSON.parse(atob(data.token.split('.')[1]));
            const userRole = decodedToken.role;

            if (userRole === 'Admin') {
                window.location.href = '../html/main.html';
            } else if (userRole === 'User') {
                window.location.href = '../html/main.html';
            } else {
                console.log(await response.text());
                alert('Произошла ошибка при авторизации');
            }            
        } else {
            console.log(await response.text());
            throw new Error('Неправильный логин или пароль');
        }
    } catch (error) {
        console.error(error);
        alert('Неправильный логин или пароль');
    }
}

async function register() {
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;

    if (!email || !password) {
        alert('Заполните необходимые поля');
        return;
    }

    const credentials = {
        email: email,
        password: password
    };

    try {
        const response = await fetch('https://localhost:7172/api/User/Register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(credentials)
        });

        if (response.ok) {
            const data = await response.json();
            localStorage.setItem('userToken', data.token);

            alert('Успешная регистрация');
            window.location.href = '/html/main.html';
        } else {
            console.log(await response.text());
            throw new Error('Ошибка регистрации');
        }
    } catch (error) {
        console.error(error);
        alert('Ошибка регистрации');
    }
}

async function updateUser() {
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;

    if (!email || !password) {
        alert('Заполните необходимые поля');
        return;
    }

    const userUpdate = {
        email: email,
        password: password,
    };

    var decodedToken = JSON.parse(atob(localStorage.getItem('userToken').split('.')[1]));
    const uid = decodedToken.nameid;

    try {
        const response = await fetch(`https://localhost:7172/api/User/UpdateUser?userUid=${uid}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${localStorage.getItem('userToken')}`,
            },
            body: JSON.stringify(userUpdate)
        });

        const data = await response.text();

        if (response.ok) {          
            console.log(data);
            alert('Данные обновлены');
            location.reload(true);
        } else {
            console.log(data);
            throw new Error('Не удалось обновить данные');
        }
    } catch (error) {
        console.error(error);
        alert('Ошибка');
    }
}

async function getUserInfo() {
    document.getElementById('password').value = '';

    var decodedToken = JSON.parse(atob(localStorage.getItem('userToken').split('.')[1]));
    const uid = decodedToken.nameid;

    try {
        const response = await fetch(`https://localhost:7172/api/User/GetUserInfo?userUid=${uid}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${localStorage.getItem('userToken')}`
            },
        });

        if (response.ok) {
            const data = await response.json();

            document.getElementById('email').value = data.email;
        } else {
            console.log(await response.text());
            throw new Error('Что-то пошло не так');
        }
    } catch (error) {
        console.error(error);
        alert('Ошибка');
    }
}