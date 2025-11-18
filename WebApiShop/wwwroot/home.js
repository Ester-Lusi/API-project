
//
async function getResponse() {
    try {
        const response = await fetch('https://localhost:44398/api/Users');
        if (response.ok) {
            const data = await response.json();
            alert(data);
        }
        else {
            throw new Error('error');
        }
    }
    catch (e) {
        console.log(e);
    }
}

//כניסת משתמש חדש
function NewUser() {
    const Email = document.querySelector("#userName");
    const firstName = document.querySelector("#firstName");
    const lastName = document.querySelector("#lastName");
    const password = document.querySelector("#password");

    const postData = {
        Email: Email.value,
        firstName: firstName.value,
        lastName: lastName.value,
        password: password.value,
    };
    return postData;
}

async function newUser() {
    try {
        const postData = NewUser();
        const response = await fetch('https://localhost:44398/api/Users', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(postData)
        });

        if (response.status == 201) {
            alert("נירשמת בהצלחה")
        }
        //צריך לשלוח לפונקציה כדי לבדוק סיסמא
        else if (response.status == 400) {
            alert("סיסמא חלשה")
        }
        else {
            alert(" לא נירשמת בהצלחה");
        }
    }
    catch (e)
    {
        console.log(e);
    }
}

//כניסת משתמש שמור
function LogUser() {
    const Email = document.querySelector("#userNameLog");
    const password = document.querySelector("#passwordLog");

    const postDataLog = {
        Email: Email.value,
        firstName: "",
        lastName: "",
        password: password.value,
    };
    return postDataLog;
}

async function Login() {
    try {

        const postDataLog = LogUser();
        const response = await fetch('https://localhost:44398/api/Users/Login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(postDataLog)
        });

        if (response.ok) {
            const data = await response.json();
            data.id =
                sessionStorage.setItem('user', JSON.stringify(data));
            alert("welcome-back");
            window.location.href = "../Login.html";
        }
        else {
            alert("שם או סיסמא שגויים");
        }
    }
    catch (e) {
        console.log(e);
    }
}

//בדיקת חוזק סיסמא
async function checkPasswordStrength() {
    try {
        const pass = document.querySelector("#password").value;
        const response = await fetch('https://localhost:44398/api/Password', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(pass)
        });
        const data = await response.json();
        const progressBar = document.querySelector(".progressBar");
        progressBar.value = data.strength*25;
        if (response.status == 200) {
            return data.strength / 4;
        }
        else {
            return 0;
        }
    }
    catch (e) {
        console.log(e);
    }
    progressBar.value = 0;
}
