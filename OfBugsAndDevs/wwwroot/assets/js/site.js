
window.initializeQuill = () => {
    document.addEventListener('DOMContentLoaded', function () {
        var quill = new Quill('#editor', {
            theme: 'snow',
            modules: {
                syntax: true,
                toolbar: [
                    [{ 'header': [1, 2, false] }],
                    ['bold', 'italic', 'underline'],
                    ['code-block']
                ]
            }
        });

        quill.on('text-change', function (delta, oldDelta, source) {
            setTimeout(() => {
                document.querySelectorAll('pre code').forEach((block) => {
                    Prism.highlightElement(block);
                });
            }, 0);
        });

        setTimeout(() => {
            document.querySelectorAll('pre code').forEach((block) => {
                Prism.highlightElement(block);
            });
        }, 0);
    });
};

// Function to apply Prism syntax highlighting
function applyPrismHighlighting() {
    document.querySelectorAll('pre code').forEach((block) => {
        Prism.highlightElement(block);
    });
}
