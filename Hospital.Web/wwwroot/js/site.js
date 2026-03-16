document.addEventListener('DOMContentLoaded', () => {
    const themeToggleBtn = document.getElementById('themeToggleBtn');
    const htmlElement = document.documentElement;

    // Check for saved theme preference in localStorage
    const currentTheme = localStorage.getItem('theme') || 'light';
    htmlElement.setAttribute('data-theme', currentTheme);
    updateButtonIcon(currentTheme);

    themeToggleBtn.addEventListener('click', () => {
        const newTheme = htmlElement.getAttribute('data-theme') === 'light' ? 'dark' : 'light';
        htmlElement.setAttribute('data-theme', newTheme);
        localStorage.setItem('theme', newTheme);
        updateButtonIcon(newTheme);
    });

    function updateButtonIcon(theme) {
        if (theme === 'dark') {
            themeToggleBtn.innerHTML = '☀️ Light Mode';
            themeToggleBtn.classList.remove('btn-outline-dark');
            themeToggleBtn.classList.add('btn-outline-light');
        } else {
            themeToggleBtn.innerHTML = '🌙 Dark Mode';
            themeToggleBtn.classList.remove('btn-outline-light');
            themeToggleBtn.classList.add('btn-outline-dark');
        }
    }
});
