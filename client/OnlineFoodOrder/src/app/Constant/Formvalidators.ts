    export const Formvalidators = {
        name: /^[a-zA-Z\s]*$/,
        firstName: /^[a-zA-Z\s]*$/,
        lastName: /^[a-zA-Z\s]*$/,
        mail: /^[a-zA-Z0-9._]+@[a-zA-Z]+\.[a-zA-Z.]{2,6}$/,
        phone: /^\+\d{2,3}-\d{10}$/,
        role: /^[0-9a-zA-Z-]*$/,
        upiId: /^[A-Za-z0-9._-]+-1@ok[A-Za-z]{3,5}$/,
        cardNumber: /^\d{16}$/,
        expiryDate: /^(0[1-9]|1[0-2])\/\d{2}$/,
        cvv: /^\d{3}$/
    };
