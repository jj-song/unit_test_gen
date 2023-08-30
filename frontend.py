import streamlit as st
import openai
import re

# This function checks if the content is a valid C# method
def is_valid_csharp_method(content):
    pattern = r"(public|private|internal|protected)(\s+static)?\s+\w+\s+\w+\("
    return re.search(pattern, content) is not None

# Modularized function to interact with an API (in this case, ChatGPT)
def send_to_api(content, endpoint):
    if endpoint == "ChatGPT":
        prompt = f"Please provide NUnit unit tests for the following C# method to maximize code coverage. Only return the code, no additional explanations:\n{content}"
        response = openai.ChatCompletion.create(
            model="gpt-3.5-turbo",
            messages=[{"role": "user", "content": prompt}],
            temperature=0.7,
            max_tokens=3000
        )
        return response['choices'][0]['message']['content']

def critique_input_method(content):
    prompt = f"Please critique the following C# method and provide suggestions for improvement:\n\n{content}. Response should only contain the critique and suggestions."
    response = openai.ChatCompletion.create(
        model="gpt-3.5-turbo",
        messages=[
            {"role": "user", "content": prompt},
            {"role": "system", "content": prompt}
        ],
        temperature=0.7,
        max_tokens=500
    )
    return response['choices'][0]['message']['content']

def main():
    st.title("Unit Test Generation Tool")

    # Input field for OpenAI API key
    openai_key = st.text_input("Enter OpenAI API Key:", type="password")
    if openai_key:
        openai.api_key = openai_key

    uploaded_file = st.file_uploader("Upload C# method (.cs file)", type=["cs"])

    # API endpoint selection dropdown
    api_endpoint = st.selectbox("Select API Endpoint", ["ChatGPT", "OtherAPI"])  # Add other API names as required

    if uploaded_file:
        content = uploaded_file.read().decode()
        # Display the uploaded C# method with syntax highlighting
        st.subheader("Uploaded C# Method")
        st.code(content, language='csharp')

        if is_valid_csharp_method(content):
            if st.button("Generate Unit Tests"):
                st.session_state.unit_tests = send_to_api(content, api_endpoint)
                # Save the result to a downloadable file
                with open("generated_tests.cs", "w") as f:
                    f.write(st.session_state.unit_tests)

            if st.button("Critique Input Method"):
                st.session_state.critique = critique_input_method(content)

            if "unit_tests" in st.session_state:
                st.subheader("Generated NUnit Tests")
                st.code(st.session_state.unit_tests, language='csharp')
                downloaded_file = open("generated_tests.cs", "r").read()
                st.download_button("Download Generated Tests", data=downloaded_file, file_name="generated_tests.cs", mime="text/plain")

            if "critique" in st.session_state:
                st.subheader("Critique of Input Method")
                st.text_area("Critique", st.session_state.critique, height=150)
        else:
            st.error("The uploaded file does not contain a valid C# method.")

if __name__ == "__main__":
    main()