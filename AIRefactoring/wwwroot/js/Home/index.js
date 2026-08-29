const refactorTips = [
    "Extract long, complex methods into smaller, descriptive helper functions to improve readability.",
    "Replace magic numbers and strings with named constants to clarify their intent.",
    "Eliminate deeply nested loops and conditionals by using early returns (guard clauses).",
    "Rename vague variables like `data` or `temp` to explicitly state what they hold.",
    "Use polymorphism instead of massive switch or if-else chains when handling conditional behavior.",
    "Remove dead code and unused variables; git history remembers what you delete.",
    "Encapsulate complex conditional expressions into well-named boolean functions."
];

function cycleTip() {
    const tipElement = document.getElementById('tip-content');

    tipElement.style.opacity = 0;

    setTimeout(() => {
        const randomTip = refactorTips[Math.floor(Math.random() * refactorTips.length)];
        tipElement.textContent = randomTip;
        tipElement.style.opacity = 1;
    }, 300);
}

cycleTip();
setInterval(cycleTip, 5000);