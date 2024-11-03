window.initializeQuillEditor = function (containerId, content) {
    const container = document.getElementById(containerId);
    if (!container) {
        console.error("Quill container not found");
        return;
    }

    console.log("Initializing Quill with content:", content); // Debug line

    // Initialize the Quill editor with syntax highlighting and set content
    const quill = new Quill(container, {
        theme: 'snow',
        modules: {
            syntax: {
                highlight: text => hljs.highlightAuto(text).value
            },
            toolbar: [
                [{ header: [1, 2, 3, 4, 5, false] }],
                ['bold', 'italic', 'underline', 'strike'],
                [{ 'color': [] }, { 'background': [] }],
                [{ 'list': 'ordered' }, { 'list': 'bullet' }],
                ['link', 'image', 'code-block']
            ]
        },
        placeholder: 'Enter content...'
    });

    // Load content if available
    if (content) {
        quill.clipboard.dangerouslyPasteHTML(content); // Use Quill’s HTML paste
        console.log("Content loaded into Quill"); // Debug line
    } else {
        console.warn("No content provided to Quill");
    }

    // Store the editor instance globally for retrieval
    window.quillInstances = window.quillInstances || {};
    window.quillInstances[containerId] = quill;
};
