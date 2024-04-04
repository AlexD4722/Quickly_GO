type authButton = {
    type: 'submit' | 'reset' | 'button',
    children: React.ReactNode
}

export default function Button ({ type, children } : authButton) {
    return (
        <>
            <button type={type} className="bg-[#4eac6d] w-full py-5 rounded-md font-semibold text-white">
                {children}
            </button>
        </>
    )
}