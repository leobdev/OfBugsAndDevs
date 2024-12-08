export function onLoad() {
    const quill = new Quill('editor', {
        modules: {
            syntax: true,              // Include syntax module
            toolbar: [['code-block']],  // Include button in toolbar

        },
        theme: 'snow'
    });
}

export function onUpdate() {
    console.log('Updated');
    
    
}

export function onDispose() {
    console.log('Disposed');
}

/*export function initializeQuillHighlighting(editorContainerId) {
    const quill = new Quill(editorContainerId, {
        modules: {
            syntax: true,              // Include syntax module
            toolbar: [['code-block']],  // Include button in toolbar
            
        },
        theme: 'snow'
    });
}
*/




