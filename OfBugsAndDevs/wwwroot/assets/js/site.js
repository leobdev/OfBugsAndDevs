window.initializeQuillEditor = function (containerId) {
    const container = document.getElementById(containerId);
    if (!container) {
        console.error("Quill container not found");
        return;
    }

    // Initialize the Quill editor
    const quill = new Quill(container, {
        theme: 'snow', // or 'bubble'
        modules: {
            toolbar: [
                ['bold', 'italic', 'underline'], // toggled buttons
                ['link', 'image', 'code-block'], // link and image
                [{ list: 'ordered' }, { list: 'bullet' }] // lists
            ]
        }
    });
    return quill; // Optionally return the quill instance if needed
};
