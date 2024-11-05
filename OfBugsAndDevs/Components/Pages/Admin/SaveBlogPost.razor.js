export function onLoad() {
    console.log('Loaded');

    document.addEventListener("DOMContentLoaded", function () {
        if (typeof hljs !== 'undefined') {
            hljs.configure({ languages: ['csharp'] }); // Specify C# language for highlighting

            function applySyntaxHighlighting(editor) {
                editor.querySelectorAll('pre').forEach(block => {
                    hljs.highlightElement(block);
                });
            }

            window.applyHighlightingToQuillContent = (containerId) => {
                const container = document.getElementById(containerId);
                if (container) {
                    const editor = container.querySelector('.ql-editor');
                    if (editor) applySyntaxHighlighting(editor);
                }
            };
        }
    });
}

export function onUpdate() {
    console.log('Updated');
}

export function onDispose() {
    console.log('Disposed');
}




