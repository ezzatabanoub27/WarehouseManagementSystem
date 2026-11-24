
(function () {
    let confirmCallback = null;

    const modal = document.getElementById('globalConfirmModal');

    if (!modal) return;

    const content = document.getElementById('globalConfirmContent');
    const title = document.getElementById('globalConfirmTitle');
    const message = document.getElementById('globalConfirmMessage');
    const confirmButton = document.getElementById('globalConfirmButton');
    const cancelButton = document.getElementById('globalConfirmCancel');

    function close() {
        content.classList.remove('scale-100', 'opacity-100');
        content.classList.add('scale-95', 'opacity-0');

        setTimeout(() => {
            modal.classList.add('hidden');
            modal.classList.remove('flex');
            confirmCallback = null;
        }, 200);
    }

    function open(options = {}) {
        title.textContent = options.title || 'Are you sure?';
        message.textContent = options.message || 'Do you want to continue?';

        confirmButton.textContent = options.confirmText || 'Confirm';

        confirmButton.className =
            'flex-1 rounded-xl px-4 py-2.5 text-sm font-semibold text-white transition focus:outline-none focus:ring-4 ' +
            (options.type === 'danger'
                ? 'bg-red-600 hover:bg-red-700 focus:ring-red-500/20'
                : 'bg-blue-600 hover:bg-blue-700 focus:ring-blue-500/20');

        confirmCallback = options.onConfirm || null;

        modal.classList.remove('hidden');
        modal.classList.add('flex');

        requestAnimationFrame(() => {
            content.classList.remove('scale-95', 'opacity-0');
            content.classList.add('scale-100', 'opacity-100');
        });
    }

    confirmButton.addEventListener('click', async function () {
        if (!confirmCallback) {
            close();
            return;
        }

        const callback = confirmCallback;

        confirmButton.disabled = true;
        confirmButton.textContent = 'Please wait...';

        try {
            await callback();
            close();
        } catch (error) {
            console.error(error);
        } finally {
            confirmButton.disabled = false;
            confirmButton.textContent = 'Confirm';
        }
    });

    cancelButton.addEventListener('click', close);

    modal.addEventListener('click', function (event) {
        if (event.target === modal) close();
    });

    document.addEventListener('keydown', function (event) {
        if (event.key === 'Escape' && !modal.classList.contains('hidden')) {
            close();
        }
    });

    window.confirmAction = open;
})();
