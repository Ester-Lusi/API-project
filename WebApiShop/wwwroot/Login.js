


//עדכון פרטי משתמש
async function Update() {
    try {
        const Email = document.querySelector("#userName").value;
        const firstName = document.querySelector("#firstName").value;
        const lastName = document.querySelector("#lastName").value;
        const password = document.querySelector("#password").value;
        const usus = JSON.parse(sessionStorage.getItem('user'));
        const id = usus.id;

        const data = { id, Email, firstName, lastName, password };

        const response = await fetch(`api/Users/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        });

        if (response.ok) {
            sessionStorage.setItem('user', JSON.stringify(data));
            alert("עידכנו ת'פרטים שלך");
        }
        else {
            alert("לא עידכנו ת'פרטים שלך");
            throw new Error(`HTTP error status:${response.status}`);
        }
    }
    catch (e) {
        console.log(e);
    }
}