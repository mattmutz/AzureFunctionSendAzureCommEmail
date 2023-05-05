function postData() {

    var formData = new FormData();

    formData.append('name', document.getElementById('name').value);
    formData.append('email', document.getElementById('email').value);
    formData.append('message', document.getElementById('message').value);
    
    var jsonData = JSON.stringify(Object.fromEntries(formData));

    var url = ' http://localhost:7071/api/HttpPostTrigger';

    fetch(url, {
        method: 'POST',
        body: jsonData
    }).then(function (response) {
        if (response.ok) {
            return 1234;
        }
        return Promise.reject(response);
    }).then(function () {
        console.info('Success');
        document.getElementById("user-message").innerHTML = "Message sent.";
    }).catch(function (error) {
        console.warn('Something went wrong: ', error);
        document.getElementById("user-message").innerHTML = "Something went wrong.";
        document.getElementById("user-message").classList.remove("good");
        document.getElementById("user-message").classList.add("error");
    });

}

document.addEventListener("DOMContentLoaded", function() {
    const button = document.getElementById("btn-submit");
    const form = document.getElementById("contact-form");

    button.addEventListener("click", (e) => {
        e.stopPropagation();
        e.preventDefault();
        console.info('Button clicked'); 
    
        if(!form.checkValidity()) {
            console.info('form is not valid');
            document.getElementById("user-message").innerHTML = "Please fill out all fields.";
            document.getElementById("user-message").classList.remove("good");
            document.getElementById("user-message").classList.add("error");
            return false;
        }
        else {
            console.info('Form is valid. Submitting...');
            document.getElementById("user-message").classList.remove("error");
            document.getElementById("user-message").classList.add("good");
            document.getElementById("user-message").innerHTML = "Sending message...";
            postData();
            return true;
        }
    });
});