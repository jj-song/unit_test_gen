import streamlit as st
import openai
import re
from gradio_client import Client


mpt_endpoint = "https://28a2b5769c540808d3.gradio.live"

# This function checks if the content is a valid C# method
def is_valid_csharp_method(content):
    pattern = r"(public|private|internal|protected)(\s+static)?\s+\w+\s+\w+\("
    return re.search(pattern, content) is not None

# Modularized function to interact with an API (in this case, ChatGPT)
def send_to_api(content, endpoint, test_framework):
    if endpoint == "gpt-3.5-turbo":
        #prompt = f"Please provide very comprehensive set of {test_framework} unit tests for the following C# method to maximize code coverage and test important functionalities of the method. Only return the code, no additional explanations:\n{content}"
        prompt = f"Act as a .NET software developer. Please provide a very comprehensive set of {test_framework} unit tests for the following C# method to maximize code coverage and test important functionalities of the method. Only return the unit-test code, no additional explanations:\n{content}"
        response = openai.ChatCompletion.create(
            model="gpt-3.5-turbo",
            messages=[{"role": "user", "content": prompt}],
            temperature=0.7,
            max_tokens=3000
        )
        result = response['choices'][0]['message']['content']
    elif endpoint == "mosaicml/mpt-7b-instruct":
        #prompt = f"Please provide very comprehensive set of {test_framework} unit tests for the following C# method to maximize code coverage and test important functionalities of the method. Only return the code, no additional explanations:\n{content}"
        prompt = f"Act as a .NET software developer. Please provide a very comprehensive set of {test_framework} unit tests for the following C# method to maximize code coverage and test important functionalities of the method. Only return the unit-test code, no additional explanations:\n{content}"
        client = Client(mpt_endpoint)
        #params = '{"max_new_tokens": 1200, "temperature": 0.3}'  # Use a single dictionary
        params = '{"max_new_tokens": 1200, "temperature": 0.73}' #, "do_sample": true, "top_p": 0.75, "top_k": 50}'  # Use a single dictionary
        result = client.predict(prompt, params, api_name="/greet")
    return result
        
        
def explain_code(content, endpoint):
    """Function to send code to the API for explanation."""
    prompt = f"Please explain the following C# method:\n\n{content}. Only return the explanation, no code."
    if endpoint == "gpt-3.5-turbo":
        response = openai.ChatCompletion.create(
            model="gpt-3.5-turbo",
            messages=[{"role": "user", "content": prompt}],
            temperature=0.7,
            max_tokens=1000
        )
        result = response['choices'][0]['message']['content']
    elif endpoint == "mosaicml/mpt-7b-instruct":
        client = Client(mpt_endpoint)
        params = '{"max_new_tokens": 1200, "temperature": 0.73}'
        result = client.predict(prompt, params, api_name="/greet")
    return result
        

def critique_input_method(content, endpoint):
    prompt = f"Please critique the following C# method and provide suggestions for improvement:\n\n{content}. Response should only contain the critique and suggestions. Do not return the code."
    if endpoint == "gpt-3.5-turbo": 
        response = openai.ChatCompletion.create(
            model="gpt-3.5-turbo",
            messages=[
                {"role": "user", "content": prompt},
                {"role": "system", "content": prompt}
            ],
            temperature=0.7,
            max_tokens=500
        )
        result = response['choices'][0]['message']['content']
    elif endpoint == "mosaicml/mpt-7b-instruct":
        client = Client(mpt_endpoint)
        params = '{"max_new_tokens": 1200, "temperature": 0.73}'
        result = client.predict(prompt, params, api_name="/greet")
    return result
    
def translate_code(content, endpoint, target_language):
    """Function to translate code to another language using LLM."""
    prompt = f"Translate the following C# code to {target_language}:\n\n{content}. Only return the code, no additional explanations."
    if endpoint == "gpt-3.5-turbo": 
        response = openai.ChatCompletion.create(
            model="gpt-3.5-turbo",
            messages=[
                {"role": "user", "content": prompt},
                {"role": "system", "content": prompt}
            ],
            temperature=0.7,
            max_tokens=500
        )
        result = response['choices'][0]['message']['content']
    elif endpoint == "mosaicml/mpt-7b-instruct":
        client = Client(mpt_endpoint)
        params = '{"max_new_tokens": 1200, "temperature": 0.73}'
        result = client.predict(prompt, params, api_name="/greet")
    return result
    
    
def chat_with_code(content, endpoint, user_query):
    """Function to chat with the code using LLM."""
    prompt = f"Code:\n{content}\n\nUser: {user_query}\nCode: "
    if endpoint == "gpt-3.5-turbo": 
        response = openai.ChatCompletion.create(
            model="gpt-3.5-turbo",
            messages=[{"role": "user", "content": prompt}],
            temperature=0.7,
            max_tokens=1000
        )
        result = response['choices'][0]['message']['content']
    elif endpoint == "mosaicml/mpt-7b-instruct":
        client = Client(mpt_endpoint)
        params = '{"max_new_tokens": 1200, "temperature": 0.73}'
        result = client.predict(prompt, params, api_name="/greet")
    return result


def main():


    # Display the centered logo
    col1, col2, col3 = st.columns([1, 2, 1])
    with col2:
        st.image("imgs/tioca_chal_coin.png", width=300)  # Adjust width as needed

    st.title("TIOCA Code Generation Tool")
    # Organize "Select API Endpoint", "Select Test Framework", and "Select Target Language" in the same row
    col1, col2, col3 = st.columns(3)
    with col1:
        api_endpoint = st.selectbox("Select API Endpoint", ["mosaicml/mpt-7b-instruct", "gpt-3.5-turbo"])
    
    # Conditionally display the OpenAI API key input based on the selected API endpoint
    if api_endpoint == "gpt-3.5-turbo":
        openai_key = st.text_input("Enter OpenAI API Key:", type="password")
        if openai_key:
            openai.api_key = openai_key

    with col2:
        test_framework = st.selectbox("Select Test Framework", ["NUnit", "xUnit.net", "MSTest"])
    
    with col3:
        target_language = st.selectbox("Select Target Language for Translation", ["Python", "Java", "JavaScript", "C++"])

    # Upload C# method
    uploaded_file = st.file_uploader("Upload C# method (.cs file)", type=["cs"])

    if uploaded_file:
        content = uploaded_file.read().decode()
        st.markdown("---")
        st.subheader("Uploaded C# Method")
        st.code(content, language='csharp')

        if is_valid_csharp_method(content):
            # Organize the buttons side by side
            btn_col1, btn_col2, btn_col3, btn_col4 = st.columns(4)
            with btn_col1:
                if st.button("Generate Unit Tests"):
                    with st.spinner('Generating Unit Tests...'):
                        st.session_state.unit_tests = send_to_api(content, api_endpoint, test_framework)
                    with open("generated_tests.cs", "w") as f:
                        f.write(st.session_state.unit_tests)

            with btn_col2:
                if st.button("Explain the Code"):
                    with st.spinner('Explaining the Code...'):
                        st.session_state.explanation = explain_code(content, api_endpoint)

            with btn_col3:
                if st.button("Review the Code"):
                    with st.spinner('Reviewing the Code...'):
                        st.session_state.critique = critique_input_method(content, api_endpoint)

            with btn_col4:
                if st.button("Translate the Code"):
                    with st.spinner('Translating the Code...'):
                        st.session_state.translated_code = translate_code(content, api_endpoint, target_language)

            # Display results
            if "unit_tests" in st.session_state:
                st.markdown("---")
                st.subheader(f"Generated {test_framework} Tests")
                st.code(st.session_state.unit_tests, language='csharp')
                downloaded_file = open("generated_tests.cs", "r").read()
                st.download_button("Download Generated Tests", data=downloaded_file, file_name="generated_tests.cs", mime="text/plain")

            if "critique" in st.session_state:
                st.markdown("---")
                st.subheader("Review of Input Method")
                st.text_area("Review", st.session_state.critique, height=150)

            if "explanation" in st.session_state:
                st.markdown("---")
                st.subheader("Explanation of Input Method")
                st.text_area("Explanation", st.session_state.explanation, height=150)

            if "translated_code" in st.session_state:
                st.markdown("---")
                st.subheader(f"Translated Code ({target_language})")
                st.code(st.session_state.translated_code, language=target_language.lower())

            # Chat with code section
            st.markdown("---")
            st.subheader("Chat with the Code")
            user_query = st.text_input("Ask a question about the code:", key="chat_input")

            if user_query and not "last_query" in st.session_state:
                st.session_state.last_query = user_query
                with st.spinner("Waiting for response..."):
                    response = chat_with_code(content, api_endpoint, user_query)
                    st.session_state.chat_response = response
                st.text(st.session_state.chat_response)
            elif "last_query" in st.session_state and st.session_state.last_query != user_query:
                # Reset last_query to allow new queries
                del st.session_state.last_query

        else:
            st.error("The uploaded file does not contain a valid C# method.")

if __name__ == "__main__":
    main()



