export function onLoad() {
    console.log("onLoad function called.");

    console.log("Checking if Highlight.js is loaded:", typeof hljs !== "undefined");

    const codeBlocks = document.querySelectorAll("pre code");
    console.log("Number of <code> blocks found:", codeBlocks.length);

    if (typeof hljs !== "undefined" && codeBlocks.length > 0) {
        codeBlocks.forEach((block) => {
            hljs.highlightElement(block);
            console.log("Highlighting applied to:", block);
        });
    } else {
        console.log("Highlight.js is not loaded or no code blocks found.");
    }
}


export function onUpdate() {
    console.log('Updated');
}

export function onDispose() {
    console.log('Disposed');
}

