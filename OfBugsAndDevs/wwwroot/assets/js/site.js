function initializeQuillHighlighting(containerClass) {
    // Find the Quill editor container using the provided class or ID
    const editor = document.querySelector(`.${containerClass} .ql-editor`);

    if (!editor) return;

    // Add syntax highlighting to code blocks on text changes
    editor.addEventListener("input", () => {
        const codeBlocks = editor.querySelectorAll("pre code");
        codeBlocks.forEach((block) => {
            hljs.highlightElement(block);
        });
    });

    // Highlight existing code blocks on initialization
    const initialCodeBlocks = editor.querySelectorAll("pre code");
    initialCodeBlocks.forEach((block) => {
        hljs.highlightElement(block);
    });
}
